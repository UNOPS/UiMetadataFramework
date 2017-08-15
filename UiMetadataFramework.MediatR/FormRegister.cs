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
		private readonly ConcurrentDictionary<string, FormInfo> registeredForms = new ConcurrentDictionary<string, FormInfo>();
		private readonly List<string> registeredAssemblies = new List<string>();
		private readonly object key = new object();

		public FormRegister(MetadataBinder binder)
		{
			this.binder = binder;
		}

		/// <summary>
		/// Gets list of all registered forms.
		/// </summary>
		public IEnumerable<FormInfo> RegisteredForms => this.registeredForms.Values;

		/// <summary>
		/// Gets <see cref="FormInfo"/> by form's id.
		/// </summary>
		/// <param name="id">Id of the form.</param>
		/// <returns>FormMetadata instance.</returns>
		public FormInfo GetFormInfo(string id)
		{
			this.registeredForms.TryGetValue(id, out FormInfo formInfo);
			return formInfo;
		}

		/// <summary>
		/// Scans assembly for all forms and adds them to the register.
		/// </summary>
		/// <param name="assembly">Assembly to scan.</param>
		public void RegisterAssembly(Assembly assembly)
		{
			// Avoid registering the same assembly twice.
			lock (this.key)
			{
				if (this.registeredAssemblies.Contains(assembly.FullName))
				{
					return;
				}

				this.registeredAssemblies.Add(assembly.FullName);
			}

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
				var iformInterfaces = form.Type.GetTypeInfo()
					.GetInterfaces()
					.Select(t => t)
					.Where(t =>
					{
						if (!t.IsConstructedGenericType)
						{
							return false;
						}

						var type = t.GetGenericTypeDefinition();
						return
							type == typeof(IForm<,,>) ||
							type == typeof(IAsyncForm<,,>);
					})
					.ToList();

				if (iformInterfaces.Count == 0)
				{
					throw new InvalidConfigurationException(
						$"Type '{form.Type.FullName}' was decorated with FormAttribute, but does not implement IForm<,,> or IAsyncForm<,,> interface.");
				}

				if (iformInterfaces.Count > 1)
				{
					throw new InvalidConfigurationException($"Type '{form.Type.FullName}' implements multiple IForm<,,> and/or IAsyncForm<,,> interfaces. Only one of these interfaces can be implemented.");
				}

				var iformInterface = iformInterfaces.Single();
				var requestType = iformInterface.GetTypeInfo().GenericTypeArguments[0];
				var responseType = iformInterface.GenericTypeArguments[1];

				this.registeredForms.TryAdd(form.Type.FullName,
					new FormInfo
					{
						FormType = form.Type,
						RequestType = requestType,
						ResponseType = responseType,
						Metadata = new FormMetadata
						{
							Label = form.Attribute.Label,
							Id = form.Type.FullName,
							PostOnLoad = form.Attribute.PostOnLoad,
							PostOnLoadValidation = form.Attribute.PostOnLoadValidation,
							OutputFields = this.binder.BindOutputFields(responseType).ToList(),
							InputFields = this.binder.BindInputFields(requestType).ToList(),
							CustomProperties = form.Attribute.GetCustomProperties()
						}
					});
			}
		}
	}
}