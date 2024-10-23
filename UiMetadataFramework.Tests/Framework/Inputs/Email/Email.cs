namespace UiMetadataFramework.Tests.Framework.Inputs.Email;

[MyInputComponent("email", DefaultLabel = "Email address")]
public class Email
{
	public string? Value { get; set; }
}