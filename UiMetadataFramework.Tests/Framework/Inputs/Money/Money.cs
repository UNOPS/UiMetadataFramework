namespace UiMetadataFramework.Tests.Framework.Inputs.Money;

using UiMetadataFramework.Core.Binding;

[InputComponent("money", typeof(MoneyAttribute))]
public class Money
{
	public decimal Amount { get; set; }
}