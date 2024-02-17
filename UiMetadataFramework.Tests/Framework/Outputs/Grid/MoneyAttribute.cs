namespace UiMetadataFramework.Tests.Framework.Outputs.Grid;

using System;
using UiMetadataFramework.Core.Binding;

public class MoneyAttribute() : CustomPropertyAttribute(PropertyName)
{
	public const string PropertyName = "money";

	public int DecimalPlaces { get; set; } = 2;
	public string? Locale { get; set; }

	public override object GetValue(Type type, MetadataBinder binder)
	{
		return new
		{
			this.DecimalPlaces,
			this.Locale
		};
	}
}