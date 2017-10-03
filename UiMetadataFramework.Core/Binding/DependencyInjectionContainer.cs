namespace UiMetadataFramework.Core.Binding
{
	using System;

	public class DependencyInjectionContainer
	{
		public DependencyInjectionContainer()
		{
		}

		public DependencyInjectionContainer(Func<Type, object> getInstanceFunc)
		{
			this.GetInstanceFunc = getInstanceFunc;
		}

		/// <summary>
		/// Default dependency injection container which always uses the default constructor
		/// when attempting to create an instance of a type.
		/// </summary>
		public static DependencyInjectionContainer Default { get; set; } = new DependencyInjectionContainer(Activator.CreateInstance);

		public Func<Type, object> GetInstanceFunc { get; set; }

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
	}
}