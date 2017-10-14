namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;
	using UiMetadataFramework.MediatR;

	public partial class BinderTests
	{
		public class MyForm : FormAttribute
		{
			public override object GetCustomProperties(Type type)
			{
				var menuAttribute = type.GetTypeInfo().GetCustomAttribute<MediatrTests.MenuAttribute>();

				return new MediatrTests.MyFormCustomProperties
				{
					ParentMenu = menuAttribute?.ParentMenu
				};
			}
		}
	}
}