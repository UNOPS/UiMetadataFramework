namespace UiMetadataFramework.Basic
{
	using System;
	using UiMetadataFramework.Basic.Input.Typeahead;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Barebone implementation of the <see cref="DependencyInjectionContainer"/>, which
	/// simply uses <see cref="Activator.CreateInstance(System.Type)"/> function. This is the most
	/// simple DI container required for UiMetadataFramework.Basic.
	/// </summary>
	public class DefaultDependencyInjectionContainer : DependencyInjectionContainer
	{
		public DefaultDependencyInjectionContainer()
		{
			this.GetInstanceFunc = t =>
			{
				if (t == typeof(DependencyInjectionContainer))
				{
					return this;
				}

				if (t == typeof(MultiSelectInputFieldBinding))
				{
					return new MultiSelectInputFieldBinding(this);
				}

				if (t == typeof(TypeaheadInputFieldBinding))
				{
					return new TypeaheadInputFieldBinding(this);
				}

				return Activator.CreateInstance(t);
			};
		}
	}
}