namespace UiMetadataFramework.Core.Binding
{
	using System;

	/// <summary>
	/// Represents a dependency injection container that will be used by the framework.
	/// </summary>
	public class DependencyInjectionContainer : IServiceProvider
	{
		/// <summary>
		/// Creates a new instance of the <see cref="DependencyInjectionContainer"/> class
		/// that will use the default constructor when attempting to create an instance of a type.
		/// </summary>
		public DependencyInjectionContainer()
		{
		}

		/// <summary>
		/// Initializes a new <see cref="DependencyInjectionContainer"/> that will use
		/// the provided function as a factory function.
		/// </summary>
		/// <param name="getInstanceFunc">Function that will get instances of a type.</param>
		public DependencyInjectionContainer(Func<Type, object> getInstanceFunc)
		{
			this.GetInstanceFunc = getInstanceFunc;
		}

		/// <summary>
		/// Default dependency injection container which always uses the default constructor
		/// when attempting to create an instance of a type.
		/// </summary>
		public static DependencyInjectionContainer Default { get; set; } = new DependencyInjectionContainer(Activator.CreateInstance);

		/// <summary>
		/// Factory function that will be used to get instances of a type.
		/// </summary>
		public Func<Type, object> GetInstanceFunc { get; set; } = null!;

		/// <summary>
		/// Instantiates the given type.
		/// </summary>
		/// <param name="type">Type of object to instantiate.</param>
		/// <returns>Instance of <paramref name="type"/>.</returns>
		public object GetInstance(Type type)
		{
			return this.GetInstanceFunc(type);
		}

		/// <summary>
		/// Instantiates the given type.
		/// </summary>
		/// <typeparam name="T"> name="type">Type of object to instantiate.</typeparam>
		/// <returns>Instance of <typeparamref name="T"/>.</returns>
		public T GetInstance<T>()
		{
			return (T)this.GetInstance(typeof(T));
		}

		public object GetService(Type serviceType)
		{
			return this.GetInstance(serviceType);
		}
	}
}