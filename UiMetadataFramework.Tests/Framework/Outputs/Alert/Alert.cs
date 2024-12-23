namespace UiMetadataFramework.Tests.Framework.Outputs.Alert;

using UiMetadataFramework.Basic.Output;

[MyOutputComponent("alert", DefaultOrderIndex = -10)]
public class Alert
{
	public string? Message { get; set; }
}