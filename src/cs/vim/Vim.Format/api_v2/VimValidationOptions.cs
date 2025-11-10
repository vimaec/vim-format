namespace Vim.Format.api_v2
{
    public class VimValidationOptions
    {
        /// <summary>
        /// Validation will fail if duplicate storage keys are detected. Setting this to false simplifies merged VIM validation.
        /// </summary>
        public bool UniqueStorageKeys { get; set; } = true;

        /// <summary>
        /// By default, we expect at least one BimDocument entity. Setting this to false suppresses this requirement. This is typically
        /// useful when stripping the bim data from the VIM file to keep it as light as possible.
        /// </summary>
        public bool BimDocumentMustExist { get; set; } = true;

        /// <summary>
        /// By default, it is recommended that the bim document element has an element id of -1.
        /// Note: this may not always be the case, particularly for IFC to VIM conversions.
        /// </summary>
        public bool BimDocumentElementMustBeSynthetic { get; set; } = true;

        /// <summary>
        /// By default, it is recommended that the bim document element has an element whose name is equal to the bim document's name.
        /// Note: this may not always be the case, particularly for IFC to VIM conversions.
        /// </summary>
        public bool BimDocumentElementNameMustMatchBimDocumentName { get; set; } = true;

        /// <summary>
        /// By default, it is recommended that the bim document element's type is set to "BimDocument Parameter Holder"
        /// Note: this may not always be the case, particularly for IFC to VIM conversions.
        /// </summary>
        public bool BimDocumentElementTypeMustBeParameterHolder { get; set; } = true;

        /// <summary>
        /// By default, it is recommended to avoid elements in systems having null values. This has been fixed as of object model v4.5.0
        /// </summary>
        public bool ElementInSystemMustNotHaveNullValues { get; set; } = true;
    }
}