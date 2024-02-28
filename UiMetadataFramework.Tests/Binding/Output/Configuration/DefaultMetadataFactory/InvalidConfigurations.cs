namespace UiMetadataFramework.Tests.Binding.Output.Configuration.DefaultMetadataFactory;

using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Framework.Outputs.Grid;
using UiMetadataFramework.Tests.Framework.Outputs.Icon;
using UiMetadataFramework.Tests.Framework.Outputs.Money;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class InvalidConfigurations
{
	public class Response
	{
		public Grid<string>? MissingMandatoryConfig { get; set; }

		[MoneyStyleData(Style = "fancy")]
		public Icon? UnrelatedConfig { get; set; }
	}

	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[Fact]
	public void MissingMandatoryConfigCausesException()
	{
		Assert.Throws<BindingException>(() => this.binder.BuildOutputComponent<Response>(t => t.MissingMandatoryConfig));
	}

	[Fact]
	public void UnrelatedConfigsNotAllowed()
	{
		Assert.Throws<BindingException>(() => this.binder.BuildOutputComponent<Response>(t => t.UnrelatedConfig));
	}
}