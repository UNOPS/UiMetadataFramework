namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;

	public class TypeaheadInputFieldBinding : InputFieldBinding
	{
		internal TypeaheadInputFieldBinding(Type serverTypes, string clientType) : base(serverTypes, clientType)
		{
		}

		public TypeaheadInputFieldBinding() : base(typeof(TypeaheadValue<>), "typeahead")
		{
		}

		public override object GetCustomProperties(InputFieldAttribute attribute, PropertyInfo property)
		{
			var typeaheadInputFieldAttribute = attribute as TypeaheadInputFieldAttribute;

			if (typeaheadInputFieldAttribute == null)
			{
				throw new BindingException($"Mandatory attribute '{typeof(TypeaheadInputFieldAttribute).FullName}' " +
					$"is missing on '{property.DeclaringType.FullName}.{property.Name}'.");
			}

			if (typeaheadInputFieldAttribute.Source.GetInterfaces(typeof(ITypeaheadRemoteSource)).Any())
			{
				return new
				{
					Source = (object)typeaheadInputFieldAttribute.Source.FullName
				};
			}

			var inlineSource = typeaheadInputFieldAttribute.Source.GetInterfaces(typeof(ITypeaheadInlineSource<>)).SingleOrDefault();
			if (inlineSource != null)
			{
				var source = Activator.CreateInstance(typeaheadInputFieldAttribute.Source);
				var items = typeaheadInputFieldAttribute.Source.GetTypeInfo().GetMethod(nameof(ITypeaheadInlineSource<int>.GetItems)).Invoke(source, null);

				return new
				{
					Source = items
				};
			}

			throw new BindingException($"Field '{property.DeclaringType.FullName}.{property.Name}' is bound to an invalid typeahead source.");
		}
	}

	public class TypeaheadValue<T>
	{
		public T Value { get; set; }
	}

	public class TypeaheadInputFieldAttribute : InputFieldAttribute
	{
		public TypeaheadInputFieldAttribute(Type source)
		{
			this.Source = source;
		}

		public Type Source { get; }
	}
}