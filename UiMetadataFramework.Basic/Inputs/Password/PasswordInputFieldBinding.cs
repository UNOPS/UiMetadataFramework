namespace UiMetadataFramework.Basic.Inputs.Password
{
	/// <summary>
	/// Represents a password input field.
	/// </summary>
	[MyInputComponent("password")]
	public class Password
	{
		/// <summary>
		/// The password value.
		/// </summary>
		public string? Value { get; set; }
	}
}