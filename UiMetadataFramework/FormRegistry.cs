namespace UiMetadataFramework.Core
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core.Metadata;

	public class FormRegistry
	{
		public static FormRegistry Default = new FormRegistry();
		private readonly ConcurrentDictionary<string, Type> registeredForms = new ConcurrentDictionary<string, Type>();
		public IEnumerable<Type> RegisteredForms => this.registeredForms.Values;

		public Type GetForm(string name)
		{
			this.registeredForms.TryGetValue(name.ToLowerInvariant(), out Type type);
			return type;
		}

		public void RegisterAssembly(Assembly assembly)
		{
			// Get all forms within the assembly.
			var forms = assembly.ExportedTypes
				.Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsGenericType)
				.Where(t => t.GetTypeInfo().GetInterfaces().Any(i => i == typeof(IFormMetadata)))
				.ToList();

			foreach (var form in forms)
			{
				this.registeredForms.TryAdd(form.Name.ToLowerInvariant(), form);
			}
		}
	}
}