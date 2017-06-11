namespace UiMetadataFramework.MediatR
{
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using UiMetadataFramework.Core;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// This class represents a register, which holds collection of form metadata.
	/// </summary>
	public class FormRegister
	{
		private readonly MetadataBinder binder;
		private readonly ConcurrentDictionary<string, FormMetadata> registeredForms = new ConcurrentDictionary<string, FormMetadata>();

		public FormRegister(MetadataBinder binder)
		{
			this.binder = binder;
		}

		/// <summary>
		/// Gets list of all registered forms.
		/// </summary>
		public IEnumerable<FormMetadata> RegisteredForms => this.registeredForms.Values;

		/// <summary>
		/// Gets form with the specified id.
		/// </summary>
		/// <param name="id">Id of the form.</param>
		/// <returns>FormMetadata instance.</returns>
		public FormMetadata GetForm(string id)
		{
			return this.registeredForms[id];
		}

		/// <summary>
		/// Scans assembly for all forms and adds them to the register.
		/// </summary>
		/// <param name="assembly">Assembly to scan.</param>
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
					.Select(t => t)
					.SingleOrDefault(t =>
					{
						var type = t.GetGenericTypeDefinition();
						return type == typeof(IForm<,>) || type == typeof(IAsyncForm<,>);
					});

				if (iformInterface == null)
				{
					throw new InvalidConfigurationException(
						$"Type '{form.Type.FullName}' was decorated with FormAttribute, but does not implement IForm<,> interface.");
				}

				var requestType = iformInterface.GetTypeInfo().GenericTypeArguments[0];
				var responseType = iformInterface.GetTypeInfo().GenericTypeArguments[1];

				this.registeredForms.TryAdd(form.Type.FullName, new FormMetadata
				{
					Label = form.Attribute.Label,
					Id = form.Type.FullName,
					PostOnLoad = form.Attribute.PostOnLoad,
					OutputFields = this.binder.BindOutputFields(responseType).ToList(),
					InputFields = this.binder.BindInputFields(requestType).ToList()
				});
			}
		}
	}
}