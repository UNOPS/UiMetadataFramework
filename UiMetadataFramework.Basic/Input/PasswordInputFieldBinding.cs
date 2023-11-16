namespace UiMetadataFramework.Basic.Input
{
    using UiMetadataFramework.Core.Binding;

    /// <summary>
    /// Represents a password input field.
    /// </summary>
    [InputFieldType("password")]
    public class Password
    {
        /// <summary>
        /// The password value.
        /// </summary>
        public string? Value { get; set; }
    }
}