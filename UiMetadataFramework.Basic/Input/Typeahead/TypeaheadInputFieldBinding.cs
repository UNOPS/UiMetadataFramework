namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Binding;

	public class TypeaheadInputFieldBinding : InputFieldBinding
	{
		internal TypeaheadInputFieldBinding(Type serverType, string clientType, DependencyInjectionContainer container)
			: base(serverType, clientType)
		{
			this.Container = container;
		}

		public TypeaheadInputFieldBinding(DependencyInjectionContainer container)
			: this(typeof(TypeaheadValue<>), "typeahead", container)
		{
		}

		public DependencyInjectionContainer Container { get; private set; }

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
				return new TypeaheadCustomProperties
				{
					Source = typeaheadInputFieldAttribute.Source.FullName
				};
			}

			var inlineSource = typeaheadInputFieldAttribute.Source.GetInterfaces(typeof(ITypeaheadInlineSource<>)).SingleOrDefault();
			if (inlineSource != null)
			{
				var source = this.Container.GetInstance(typeaheadInputFieldAttribute.Source);
				var items = typeaheadInputFieldAttribute.Source.GetTypeInfo().GetMethod(nameof(ITypeaheadInlineSource<int>.GetItems)).Invoke(source, null);

				return new TypeaheadCustomProperties
				{
					Source = items
				};
			}

			throw new BindingException($"Field '{property.DeclaringType.FullName}.{property.Name}' is bound to an invalid typeahead source.");
		}
	}

	public class TypeaheadCustomProperties
	{
		public object Source { get; set; }
	}

	public class TypeaheadValue<T>
	{
		public TypeaheadValue()
		{
		}

		public TypeaheadValue(T value)
		{
			this.Value = value;
		}

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