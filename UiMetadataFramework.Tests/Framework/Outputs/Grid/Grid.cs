namespace UiMetadataFramework.Tests.Framework.Outputs.Grid;

using UiMetadataFramework.Core.Binding;

[OutputComponent("grid")]
[HasConfiguration(typeof(GridAttribute), mandatory: true)]
public class Grid<T>
{
	public T? Value { get; set; }
}