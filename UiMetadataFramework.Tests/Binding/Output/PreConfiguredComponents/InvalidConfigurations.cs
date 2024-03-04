namespace UiMetadataFramework.Tests.Binding.Output.PreConfiguredComponents;

using System.Linq;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.ObjectList;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class InvalidConfigurations
{
	public class Outputs
	{
		[ObjectList(Gap = "10")]
		public StarPointList<int>? ListOfLists { get; set; }
	}

	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[ObjectList(Style = "bullet-point-list", ListItem = "*")]
	public class BulletPointList<T> : ObjectList<T>;

	[ObjectList(Style = "start-point-list")]
	public class StarPointList<T> : BulletPointList<T>;

	[Fact]
	public void MultilevelConfigurationNotAllowed()
	{
		Assert.Throws<BindingException>(() => this.binder.Outputs.GetFields(typeof(Outputs)).ToList());
	}
}