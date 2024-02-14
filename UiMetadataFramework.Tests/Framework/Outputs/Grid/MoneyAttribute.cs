namespace UiMetadataFramework.Tests.Framework.Outputs.Grid;

using UiMetadataFramework.Core.Binding;

public class MoneyAttribute() : CustomPropertyAttribute(PropertyName)
{
	public const string PropertyName = "money";

	public int DecimalPlaces { get; set; } = 2;
	public string? Locale { get; set; }

	public override object GetValue()
	{
		return new
		{
			this.DecimalPlaces,
			this.Locale
		};
	}
}