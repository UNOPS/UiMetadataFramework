namespace UiMetadataFramework.Tests.Utilities;

using System.Reflection;
using UiMetadataFramework.Basic;
using UiMetadataFramework.Basic.Inputs;
using UiMetadataFramework.Basic.Output;
using UiMetadataFramework.Basic.Output.Text;
using UiMetadataFramework.Core.Binding;
using UiMetadataFramework.Tests.Binding.Form;

public static class MetadataBinderFactory
{
	public static MetadataBinder CreateMetadataBinder()
	{
		var binder = new MetadataBinder(
			new DefaultDependencyInjectionContainer(),
			new MetadataBinderConfiguration(
				new MyInputFieldMetadataFactory(),
				new MyOutputFieldMetadataFactory()));

		binder.RegisterAssembly(typeof(StringOutputComponentBinding).GetTypeInfo().Assembly);
		binder.RegisterAssembly(typeof(FormBindingTests).GetTypeInfo().Assembly);

		return binder;
	}
}