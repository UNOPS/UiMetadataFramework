namespace UiMetadataFramework.Basic
{
	using System;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Bare-bone implementation of the <see cref="DependencyInjectionContainer"/>, which
	/// simply uses <see cref="Activator.CreateInstance(System.Type)"/> function. This is the most
	/// simple DI container required for UiMetadataFramework.Basic.
	/// </summary>
	public class DefaultDependencyInjectionContainer : DependencyInjectionContainer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultDependencyInjectionContainer"/> class.
		/// </summary>
		public DefaultDependencyInjectionContainer()
		{
			this.GetInstanceFunc = t =>
			{
				if (t == typeof(DependencyInjectionContainer))
				{
					return this;
				}

				return Activator.CreateInstance(t);
			};
		}
	}
}