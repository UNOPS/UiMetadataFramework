namespace UiMetadataFramework.Tests.Binding.Output.PreConfiguredComponents;

using System.Linq;
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

	public class HighPrecisionMoney : IPreConfiguredComponent<Money>
	{
		[Money(8)]
		[MoneyStyleItem(Style = "precise")]
		public Money? Value { get; set; }
	}

	[Fact]
	public void NestedPreConfiguredComponentBound()
	{
		var money = this.binder.BuildOutputFields<Outputs>()
			.Single(t => t.Id == nameof(Outputs.Amounts)).Component.GetConfigurationOrException<ObjectListAttribute.Configuration>()
			.InnerComponent;

		Assert.Equal("money", money.Type);

		var moneyConfig = money.Configuration as MoneyAttribute.Configuration;

		Assert.NotNull(moneyConfig);
		Assert.Equal(8, moneyConfig!.DecimalPlaces);
		Assert.Equal("precise", moneyConfig.Style);
	}

	[Fact]
	public void OuterGenericComponentBound()
	{
		var objectList = this.binder.BuildOutputFields<Outputs>()
			.Single(t => t.Id == nameof(Outputs.Amounts));

		var objectListConfig = objectList.Component.GetConfigurationOrException<ObjectListAttribute.Configuration>();

		Assert.Equal("object-list", objectList.Component.Type);
		Assert.Equal("bullet-point-list", objectListConfig.Style);
	}
}