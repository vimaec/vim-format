# VIM Format Samples

This folder contains a collection of code samples which make use of the VIM file format C# API. If you are new to VIM, we invite you to [get started here](https://docs.vimaec.com/docs/vim-for-windows/getting-started).

Note that the projects in this folder reference the C# project [Vim.Format.csproj](../vim/Vim.Format/Vim.Format.csproj) directly to programatically interact with VIM files. You can clone this repository, or follow along in your own C# projects using the [Vim.Format](https://www.nuget.org/packages/Vim.Format) NuGet package.

The sections below describe each sample in more detail.

## Vim.JsonDigest

**Concepts**

This sample illustrates how to extract BIM data from a VIM file and how to transform it into a JSON payload. This can be useful in an automated data analysis pipeline where only specific information is required from a VIM file.

**Projects**

[Vim.JsonDigest.csproj](./Vim.JsonDigest/Vim.JsonDigest.csproj)

  - Defines the class [VimJsonDigest.cs](./Vim.JsonDigest/VimJsonDigest.cs) which opens a VIM file from a seekable `Stream` and aggregates [room](./Vim.JsonDigest/RoomInfo.cs), [area](./Vim.JsonDigest/AreaInfo.cs), and [material](./Vim.JsonDigest/MaterialInfo.cs) information contained within the VIM file. Provides a `.ToJson()` function to convert this aggregate information into a JSON string.

[Vim.JsonDigest.Tests.csproj](./Vim.JsonDigest.Tests/Vim.JsonDigest.Tests.csproj)

  - NUnit test project used to validate [VimJsonDigest.cs](./Vim.JsonDigest/VimJsonDigest.cs).

[Vim.JsonDigest.AzureFunction.csproj](./Vim.JsonDigest.AzureFunction/Vim.JsonDigest.AzureFunction.csproj)

  - An Azure function which downloads the VIM file from the given `vim_url` and returns a JSON payload containing the aggregate room, area, and material information contained in the VIM file.

  - Run the project within Visual Studio 2022 (tested on v17.9.7), then open your web browser at http://localhost:7071/api/GetVimJsonDigest?vim_url=https://vimdevelopment01storage.blob.core.windows.net/samples/RoomTest.vim.
  
    - Upon success, you will receive a JSON response describing the aggregate information contained in the VIM file.

  
