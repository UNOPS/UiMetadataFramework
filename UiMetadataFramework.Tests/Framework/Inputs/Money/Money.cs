namespace UiMetadataFramework.Tests.Framework.Inputs.Money;

using UiMetadataFramework.Core.Binding;

[InputComponent("money")]
[HasConfiguration(typeof(MoneyAttribute), mandatory: true)]
public class Money
{
	public decimal Amount { get; set; }
}