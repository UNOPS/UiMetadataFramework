namespace UiMetadataFramework.Tests.Binding.Output.PreConfiguredComponents;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Framework.Outputs.ObjectList;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class GenericComponent
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	public class Outputs
	{
		public BulletPointList<HighPrecisionMoney>? Amounts { get; set; }

		public BulletPointList<string>? Names { get; set; }
	}

	public class BulletPointList<T> : IPreConfiguredComponent<ObjectList<T>>
	{
		[ObjectList(Style = "bullet-point-list", ListItem = "*")]
		public ObjectList<T>? Value { get; set; }
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
		var money = this.binder.BindOutputFields<Outputs>()
			.Single(t => t.Id == nameof(Outputs.Amounts)).Component.GetConfigurationOrException<ObjectListAttribute.Configuration>()
			.InnerComponent;

		Assert.Equal("money", money.Type);

		var moneyConfig = money.Configuration as MoneyAttribute.Configuration;

		Assert.NotNull(moneyConfig);
		Assert.Equal(8, moneyConfig!.DecimalPlaces);
		Assert.Equal("precise", moneyConfig.Style);
	}

	[Fact]
	public void NestedUnconfiguredComponentBound()
	{
		var text = this.binder.BindOutputFields<Outputs>()
			.Single(t => t.Id == nameof(Outputs.Names)).Component.GetConfigurationOrException<ObjectListAttribute.Configuration>()
			.InnerComponent;

		Assert.Equal("text", text.Type);
		Assert.Null(text.Configuration);
	}

	[Fact]
	public void OuterGenericComponentBound()
	{
		var objectList = this.binder.BindOutputFields<Outputs>()
			.Single(t => t.Id == nameof(Outputs.Amounts));

		var objectListConfig = objectList.Component.GetConfigurationOrException<ObjectListAttribute.Configuration>();

		Assert.Equal("object-list", objectList.Component.Type);
		Assert.Equal("bullet-point-list", objectListConfig.Style);
		Assert.Equal("*", objectListConfig.ListItem);
	}
}