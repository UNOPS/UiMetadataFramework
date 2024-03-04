namespace UiMetadataFramework.Tests.Binding.Output.PreConfiguredComponents;

using FluentAssertions;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Framework.Outputs.ObjectList;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class InsideGenericComponent
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	public class Outputs
	{
		[ObjectList(Style = "bullet-point-list")]
		public ObjectList<HighPrecisionMoney>? Amounts { get; set; }
	}

	[Money(8)]
	[MoneyStyle(Style = "precise")]
	public class HighPrecisionMoney : Money;

	[Fact]
	public void NestedPreConfiguredComponentBound()
	{
		var money = this.binder.BuildOutputComponent<Outputs>(t => t.Amounts)
			.ConfigAsDictionary()!
			["InnerComponent"]
			.As<Component>();

		Assert.Equal("money", money.Type);

		var moneyConfig = money.ConfigAsDictionary()!;

		Assert.NotNull(moneyConfig);
		Assert.Equal(8, moneyConfig["DecimalPlaces"]);
		Assert.Equal("precise", moneyConfig["Style"]);
	}

	[Fact]
	public void OuterGenericComponentBound()
	{
		var component = this.binder.BuildOutputComponent<Outputs>(t => t.Amounts);
		var config = component.ConfigAsDictionary()!;

		Assert.Equal("object-list", component.Type);
		Assert.Equal("bullet-point-list", config["Style"]);
	}
}