using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Vim.Util;

namespace Vim.JsonDigest.AzureFunction;

public class GetVimJsonDigest
{
    private readonly ILogger _logger;

    public GetVimJsonDigest(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<GetVimJsonDigest>();
    }

    public const string vimUrlArg = "vim_url";

    [Function(nameof(GetVimJsonDigest))]
    public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
    {
        // Check the query parameters in case a GET message was sent.
        var queryParameters = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
        var vimUrl = queryParameters[vimUrlArg];

        // Check the request body in case a POST message was sent.
        if (string.IsNullOrWhiteSpace(vimUrl))
        {
            using var reader = new StreamReader(req.Body);
            var requestBody = await reader.ReadToEndAsync();
            dynamic? data = JsonSerializer.Deserialize<Dictionary<string,string>>(requestBody);
            data?.TryGetValue(vimUrlArg, out vimUrl);
        }

        if (string.IsNullOrWhiteSpace(vimUrl))
            return await BadRequest(req, $"Please pass a URL in the {vimUrlArg} argument in the query string or in the request body");

        try
        {
            // Download the VIM file into memory and analyze it.
            using var memoryStream = new MemoryStream();

            _logger.LogInformation($"Downloading VIM file from {vimUrl}");

            var bytesDownloaded = await Http.DownloadAsync(vimUrl, memoryStream,
                bufferSize: Http.DefaultDownloadBufferSize * 10);

            _logger.LogInformation($"Downloaded {bytesDownloaded} from {vimUrl}");

            if (bytesDownloaded == 0)
                return await BadRequest(req, "VIM file is empty.");

            // Create a VIM json digest from the memory stream.
            _logger.LogTrace("Preparing to read VIM file");

            var vimJsonDigest = new VimJsonDigest(memoryStream);

            _logger.LogTrace("Created VIM json digest");

            var jsonContent = vimJsonDigest.ToJson();

            // Send the success.
            var response = req.CreateResponse(HttpStatusCode.OK);
            var responseBody = new { result = jsonContent };
            await response.WriteAsJsonAsync(responseBody);

            _logger.LogInformation("Successfully wrote VIM json digest");

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Internal error");
            return await ServerError(req, $"An error has occurred (0x{ex.HResult:x8})");
        }
    }

    private static async Task<HttpResponseData> BadRequest(HttpRequestData req, string errorMessage)
    {
        var response = req.CreateResponse(HttpStatusCode.BadRequest);
        var errorResponse = new { error = errorMessage };
        await response.WriteAsJsonAsync(errorResponse);
        return response;
    }

    private static async Task<HttpResponseData> ServerError(HttpRequestData req, string errorMessage)
    {
        var response = req.CreateResponse(HttpStatusCode.InternalServerError);
        var errorResponse = new { error = errorMessage };
        await response.WriteAsJsonAsync(errorResponse);
        return response;
    }
}
