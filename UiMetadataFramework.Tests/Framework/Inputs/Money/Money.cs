namespace UiMetadataFramework.Tests.Framework.Inputs.Money;

using System;
using UiMetadataFramework.Basic.Inputs;
using UiMetadataFramework.Core.Binding;

[MyInputComponent("money")]
[HasConfiguration(typeof(MoneyAttribute), mandatory: true)]
public class Money
{
	public decimal Amount { get; set; }

	[ComponentFunction("half")]
	public static string Half(Money amount, IServiceProvider sp)
	{
		var binder = sp.GetService(typeof(MetadataBinder));
		return amount.Amount / 2 + binder.GetType().ToString();
	}
}