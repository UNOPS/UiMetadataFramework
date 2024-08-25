namespace UiMetadataFramework.Tests.Binding.Output.Panel;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Panel;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class PanelTests
{
	public class Response1
	{
		public BoxInfo? Box { get; set; }

		public class BoxInfo : Panel
		{
			public int? Weight { get; set; }
		}
	}

	public class Response2
	{
		public UserInfo? User { get; set; }

		public class UserInfo : Panel
		{
			public string? Name { get; set; }
		}
	}

	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[Fact]
	public void EachPanelSubclassHasItsOwnMetadata()
	{
		var response1 = this.binder.Outputs.GetFields(typeof(Response1));
		var response2 = this.binder.Outputs.GetFields(typeof(Response2));

		var configObject1 = response1.AssertHasOutputField(nameof(Response1.Box)).Component.Configuration;
		var configObject2 = response2.AssertHasOutputField(nameof(Response2.User)).Component.Configuration;

		var config1 = PanelMetadataFactory.ParseConfiguration(configObject1!);
		var config2 = PanelMetadataFactory.ParseConfiguration(configObject2!);

		config1.AssertHasOutputField(nameof(Response1.BoxInfo.Weight));
		config2.AssertHasOutputField(nameof(Response2.UserInfo.Name));
	}
}