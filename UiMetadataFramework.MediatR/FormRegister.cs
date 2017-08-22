namespace UiMetadataFramework.MediatR
{
	using System;
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
		private readonly object key = new object();
		private readonly List<string> registeredAssemblies = new List<string>();
		private readonly ConcurrentDictionary<string, FormInfo> registeredForms = new ConcurrentDictionary<string, FormInfo>();

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
		/// <returns><see cref="FormInfo"/> instance or null if no metadata for the form was found.</returns>
		public FormInfo GetFormInfo(string id)
		{
			this.registeredForms.TryGetValue(id, out FormInfo formInfo);
			return formInfo;
		}

		/// <summary>
		/// Gets <see cref="FormInfo"/> by form's type.
		/// </summary>
		/// <param name="formType">Type that is decorated with <see cref="FormAttribute"/>.</param>
		/// <returns><see cref="FormInfo"/> instance or null if no metadata for the form was found.</returns>
		public FormInfo GetFormInfo(Type formType)
		{
			var attribute = formType.GetTypeInfo().GetCustomAttribute<FormAttribute>();

			if (attribute == null)
			{
				throw new InvalidConfigurationException(
					$"Type '{formType.FullName}' does not have mandatory attribute '{typeof(FormAttribute).FullName}'.");
			}

			var formId = GetFormId(formType, attribute);
			this.registeredForms.TryGetValue(formId, out FormInfo metadata);

			return metadata;
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
				this.RegisterForm(form.Type, form.Attribute);
			}
		}

		/// <summary>
		/// Adds specific form to the register.
		/// </summary>
		/// <param name="formType">Type which implements <see cref="IForm{TRequest,TResponse,TResponseMetadata}"/> 
		/// or <see cref="IAsyncForm{TRequest,TResponse,TResponseMetadata}"/>.</param>
		public void RegisterForm(Type formType)
		{
			var attribute = formType.GetTypeInfo().GetCustomAttribute<FormAttribute>();
			if (attribute == null)
			{
				throw new InvalidConfigurationException(
					$"Type '{formType.FullName}' is not decorated with the mandatory '{typeof(FormAttribute).FullName}' attribute.");
			}

			this.RegisterForm(formType, attribute);
		}

		private static string GetFormId(Type formType, FormAttribute formAttribute)
		{
			return !string.IsNullOrWhiteSpace(formAttribute.Id)
				? formAttribute.Id
				: formType.FullName;
		}

		/// <summary>
		/// Adds specific form to the register.
		/// </summary>
		/// <param name="formType">Type which implements <see cref="IForm{TRequest,TResponse,TResponseMetadata}"/> 
		/// or <see cref="IAsyncForm{TRequest,TResponse,TResponseMetadata}"/>.</param>
		/// <param name="formAttribute">Attribute decorating the form.</param>
		private void RegisterForm(Type formType, FormAttribute formAttribute)
		{
			var iformInterfaces = formType.GetTypeInfo()
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
					$"Type '{formType.FullName}' was decorated with FormAttribute, but does not implement IForm<,,> or IAsyncForm<,,> interface.");
			}

			if (iformInterfaces.Count > 1)
			{
				throw new InvalidConfigurationException(
					$"Type '{formType.FullName}' implements multiple IForm<,,> and/or IAsyncForm<,,> interfaces. Only one of these interfaces can be implemented.");
			}

			var formId = GetFormId(formType, formAttribute);

			if (this.registeredForms.TryGetValue(formId, out FormInfo existingFormInfo))
			{
				if (formType != existingFormInfo.FormType)
				{
					throw new InvalidConfigurationException(
						$"Types '{formType.FullName}' and '{existingFormInfo.FormType}' have same form ID. Each form must have a unique form ID.");
				}
			}

			var iformInterface = iformInterfaces.Single();
			var requestType = iformInterface.GetTypeInfo().GenericTypeArguments[0];
			var responseType = iformInterface.GenericTypeArguments[1];

			this.registeredForms.TryAdd(
				formId,
				new FormInfo
				{
					FormType = formType,
					RequestType = requestType,
					ResponseType = responseType,
					Metadata = new FormMetadata
					{
						Label = formAttribute.Label,
						Id = formId,
						PostOnLoad = formAttribute.PostOnLoad,
						PostOnLoadValidation = formAttribute.PostOnLoadValidation,
						OutputFields = this.binder.BindOutputFields(responseType).ToList(),
						InputFields = this.binder.BindInputFields(requestType).ToList(),
						CustomProperties = formAttribute.GetCustomProperties(formType)
					}
				});
		}
	}
}