namespace UiMetadataFramework.Core.Binding
{
	using System;

	public class DependencyInjectionContainer
	{
		public Func<Type, object> GetInstanceFunc { get; set; }

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

		public object GetInstance(Type type)
		{
			return this.GetInstanceFunc(type);
		}

		public T GetInstance<T>()
		{
			return (T)this.GetInstance(typeof(T));
		}
	}
}