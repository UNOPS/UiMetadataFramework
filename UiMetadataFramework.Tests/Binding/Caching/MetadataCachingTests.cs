namespace UiMetadataFramework.Tests.Binding.Caching;

using System.Reflection;
using UiMetadataFramework.Basic.Output.Text;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Utilities;
using Xunit;

public class MetadataCachingTests
{
	private readonly MetadataBinder binder = MetadataBinderFactory.CreateMetadataBinder();

	[Fact]
	public void DuplicateAttemptsToBindSameAssemblyAreIgnored()
	{
		this.binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);
		this.binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);
	}
}