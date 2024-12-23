namespace UiMetadataFramework.Tests.Framework.Inputs.Money;

using UiMetadataFramework.Basic.Inputs;
using UiMetadataFramework.Core.Binding;

[MyInputComponent("money")]
[HasConfiguration(typeof(MoneyAttribute), mandatory: true)]
public class Money
{
	public decimal Amount { get; set; }
}