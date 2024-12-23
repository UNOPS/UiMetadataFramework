// ReSharper disable UnusedMember.Global

namespace UiMetadataFramework.Tests.Framework.Outputs.Money;

using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Core.Binding;

[MyOutputComponent("money")]
[HasConfiguration(typeof(MoneyAttribute), mandatory: true)]
[HasConfiguration(typeof(MoneyStyleAttribute))]
public class Money
{
	public decimal Amount { get; set; }
	public string? Currency { get; set; }
}