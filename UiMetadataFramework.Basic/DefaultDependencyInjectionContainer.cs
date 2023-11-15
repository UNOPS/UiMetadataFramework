namespace UiMetadataFramework.Basic
{
	using System;
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

				return Activator.CreateInstance(t);
			};
		}
	}
}