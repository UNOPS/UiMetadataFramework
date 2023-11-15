namespace UiMetadataFramework.Tests.Binding
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;

	public partial class BinderTests
	{
		public class MyForm : FormAttribute
		{
			public override IDictionary<string, object?> GetCustomProperties(Type type)
			{
				var menuAttribute = type.GetTypeInfo().GetCustomAttribute<MediatrTests.MenuAttribute>();

				return base.GetCustomProperties(type).Set("ParentMenu", menuAttribute?.ParentMenu);
			}
		}
	}
}