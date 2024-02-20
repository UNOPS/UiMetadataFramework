// ReSharper disable UnusedMember.Global

namespace UiMetadataFramework.Tests.Framework.Outputs.Money;

using UiMetadataFramework.Core.Binding;

[OutputComponent("money", metadataFactory: typeof(MoneyAttribute))]
public class Money
{
	public decimal Amount { get; set; }
	public string? Currency { get; set; }
}