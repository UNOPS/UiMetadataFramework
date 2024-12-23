namespace UiMetadataFramework.Tests.Framework.Outputs.Grid;

using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Core.Binding;

[MyOutputComponent("grid")]
[HasConfiguration(typeof(GridAttribute), mandatory: true)]
public class Grid<T>
{
	public T? Value { get; set; }
}