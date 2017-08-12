namespace UiMetadataFramework.Basic.Input
{
	using UiMetadataFramework.Core.Binding;

	public class PasswordInputFieldBinding : InputFieldBinding
	{
		public PasswordInputFieldBinding() : base(typeof(Password), "password")
		{
		}
	}

	public class Password
	{
		public string Value { get; set; }
	}
}