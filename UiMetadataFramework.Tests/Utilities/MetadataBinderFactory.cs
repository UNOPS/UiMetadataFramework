namespace UiMetadataFramework.Tests.Utilities;

using System.Reflection;
using UiMetadataFramework.Basic;
using UiMetadataFramework.Basic.Output.Text;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Binding;

public static class MetadataBinderFactory
{
	public static MetadataBinder CreateMetadataBinder()
	{
		var binder = new MetadataBinder(new DefaultDependencyInjectionContainer());

		binder.RegisterAssembly(typeof(StringOutputFieldBinding).GetTypeInfo().Assembly);
		binder.RegisterAssembly(typeof(FormBindingTests).GetTypeInfo().Assembly);

		return binder;
	}
}