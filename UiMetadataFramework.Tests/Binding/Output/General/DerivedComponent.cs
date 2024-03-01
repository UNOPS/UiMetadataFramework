namespace UiMetadataFramework.Tests.Binding.Output.General;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Grid;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class DerivedComponent
{
	// public class Layout<T> : Grid<T>
	// {
	// 	public int Size { get; set; }
	// }
	//
	// public class Components
	// {
	// 	public Layout<Article>? Data { get; set; }
	// }
	//
	// public class Article
	// {
	// 	public string? Content { get; set; }
	// 	public string? Title { get; set; }
	// }
	//
	// private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();
	//
	// [Fact]
	// public void DerivedComponentBound()
	// {
	// 	var component = this.binder.BuildOutputComponent<Components>(t => t.Data);
	//
	// 	Assert.Equal("grid", component.Type);
	// }
}