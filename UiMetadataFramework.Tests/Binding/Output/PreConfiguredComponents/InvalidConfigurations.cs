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

	public class BulletPointList<T> : IPreConfiguredComponent<ObjectList<T>>
	{
		[ObjectList(Style = "bullet-point-list", ListItem = "*")]
		public ObjectList<T>? Value { get; set; }
	}

	public class StarPointList<T> : IPreConfiguredComponent<BulletPointList<T>>
	{
		[ObjectList(Style = "start-point-list")]
		public BulletPointList<T>? Value { get; set; }
	}

	[Fact]
	public void MultilevelConfigurationNotAllowed()
	{
		Assert.Throws<BindingException>(() => this.binder.Outputs.GetFields(typeof(Outputs)).ToList());
	}
}