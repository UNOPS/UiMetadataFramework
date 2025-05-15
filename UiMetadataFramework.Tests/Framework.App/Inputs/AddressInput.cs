namespace UiMetadataFramework.Tests.Framework.App.Inputs;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Inputs.FlexboxInput;

public class AddressInput : FlexboxInput
{
	[InputField(Required = true)]
	public string? City { get; set; }

	[InputField(Required = true)]
	public string? Country { get; set; }

	[ComponentFunction("get-description")]
	public static string GetDescription(AddressInput address)
	{
		return $"{address.City}, {address.Country}";
	}
}