using Vim.Util;

namespace Vim.Format
{
    public enum VimErrorCode
    {
        VimDataFormatError = unchecked((int)(HResult.CustomFailureMask | 0x00003000)), // starting error code range at 0xA0003000.
        VimHeaderTokenizationError,
        VimHeaderDuplicateFieldError,
        VimHeaderFieldParsingError,
        VimHeaderRequiredFieldsNotFoundError,

        VimMergeObjectModelMajorVersionMismatch,
        VimMergeConfigFilePathIsEmpty,
        VimMergeInputFileNotFound,
    }

    public class VimHeaderTokenizationException : HResultException
    {
        public VimHeaderTokenizationException(string line, int numTokens)
            : base((int) VimErrorCode.VimHeaderTokenizationError,
                $"Unexpected number of tokens: {numTokens} ({line})")
        { }
    }

    public class VimHeaderDuplicateFieldException : HResultException
    {
        public VimHeaderDuplicateFieldException(string fieldName)
            : base((int) VimErrorCode.VimHeaderDuplicateFieldError, fieldName)
        { }
    }

    public class VimHeaderFieldParsingException : HResultException
    {
        public VimHeaderFieldParsingException(string fieldName, string fieldValue)
            : base((int) VimErrorCode.VimHeaderFieldParsingError,
                $"Could not parse field {fieldName} with value {fieldValue}")
        { }
    }

    public class VimHeaderRequiredFieldsNotFoundException : HResultException
    {
        public VimHeaderRequiredFieldsNotFoundException(params string[] fieldNames)
            : base((int) VimErrorCode.VimHeaderRequiredFieldsNotFoundError, string.Join(", ", fieldNames))
        { }
    }
}
