namespace UiMetadataFramework.Tests.Framework.Inputs.Email;

using UiMetadataFramework.Basic.Inputs;

[MyInputComponent("email", DefaultLabel = "Email address")]
public class Email
{
	public string? Value { get; set; }
}