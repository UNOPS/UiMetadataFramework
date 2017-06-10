namespace UiMetadataFramework.MediatR
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	public class FormRegistry
	{
		private readonly ConcurrentDictionary<string, FormMetadata> registeredForms = new ConcurrentDictionary<string, FormMetadata>();
		private readonly MetadataBinder binder;

		public FormRegistry(MetadataBinder binder)
		{
			this.binder = binder;
		}

		public IEnumerable<FormMetadata> RegisteredForms => this.registeredForms.Values;

		public FormMetadata GetForm(string name)
		{
			FormMetadata type;
			this.registeredForms.TryGetValue(name.ToLowerInvariant(), out type);
			return type;
		}

		public void RegisterAssembly(Assembly assembly)
		{
			// Get all classes decorated with FormAttribute.
			var forms = assembly.ExportedTypes
				.Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract && !t.GetTypeInfo().IsGenericType)
				.Select(t => new
				{
					Type = t,
					Attribute = t.GetTypeInfo().GetCustomAttribute<FormAttribute>()
				})
				.Where(t => t.Attribute != null)
				.ToList();

			foreach (var form in forms)
			{
				var iformInterface = form.Type.GetTypeInfo()
					.GetInterfaces()
					.SingleOrDefault(t => t == typeof(IForm<,>));

				if (iformInterface == null)
				{
					throw new InvalidConfigurationException($"Type '{form.Type.FullName}' was decorated with FormAttribute, but does not implement IForm<,> interface.");
				}

				var requestType = iformInterface.GetTypeInfo().GenericTypeArguments[0];
				var responseType = iformInterface.GetTypeInfo().GenericTypeArguments[1];

				this.registeredForms.TryAdd(form.Type.FullName, new FormMetadata
				{
					Label = form.Attribute.Label,
					Id = form.Type.FullName,
					PostOnLoad = form.Attribute.PostOnLoad,
					OutputFields = this.binder.BindOutputFields<>()
				});
			}
		}
	}
}