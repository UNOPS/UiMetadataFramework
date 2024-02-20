namespace UiMetadataFramework.Basic.Inputs.Password
{
    using UiMetadataFramework.Core.Binding;

    /// <summary>
    /// Represents a password input field.
    /// </summary>
    [InputComponent("password")]
    public class Password
    {
        /// <summary>
        /// The password value.
        /// </summary>
        public string? Value { get; set; }
    }
}