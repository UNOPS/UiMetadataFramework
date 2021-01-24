﻿namespace UiMetadataFramework.MediatR
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using global::MediatR;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// This class represents a register, which holds collection of form metadata.
	/// </summary>
	public class FormRegister
	{
		private readonly MetadataBinder binder;
		private readonly ConcurrentDictionary<string, FormInfo> formMetadata = new ConcurrentDictionary<string, FormInfo>();
		private readonly object key = new object();
		private readonly List<string> registeredAssemblies = new List<string>();
		private readonly ConcurrentDictionary<string, Type> registeredForms = new ConcurrentDictionary<string, Type>();

		/// <summary>
		/// Initializes a new instance of the <see cref="FormRegister"/> class.
		/// </summary>
		/// <param name="binder"></param>
		public FormRegister(MetadataBinder binder)
		{
			this.binder = binder;
		}

		/// <summary>
		/// Gets list of all registered forms.
		/// </summary>
		public IEnumerable<FormInfo> RegisteredForms => this.registeredForms.Select(t => this.GetFormInfo(t.Key));

		/// <summary>
		/// Gets <see cref="FormInfo"/> by form's id.
		/// </summary>
		/// <param name="id">Id of the form.</param>
		/// <returns><see cref="FormInfo"/> instance or null if no metadata for the form was found.</returns>
		public FormInfo GetFormInfo(string id)
		{
			if (this.registeredForms.TryGetValue(id, out var formType))
			{
				return this.formMetadata.GetOrAdd(id, t => this.BuildFormMetadata(formType));
			}

			return null;
		}

		/// <summary>
		/// Gets <see cref="FormInfo"/> by form's type.
		/// </summary>
		/// <param name="formType">Type that is decorated with <see cref="FormAttribute"/>.</param>
		/// <returns><see cref="FormInfo"/> instance or null if no metadata for the form was found.</returns>
		public FormInfo GetFormInfo(Type formType)
		{
			var formId = formType.GetFormId();
			return this.GetFormInfo(formId);
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
				this.RegisterForm(form.Type);
			}
		}

		/// <summary>
		/// Adds specific form to the register.
		/// </summary>
		/// <param name="formType">Type which implements <see cref="Form{TRequest,TResponse,TResponseMetadata}"/> 
		/// or <see cref="AsyncForm{TRequest,TResponse,TResponseMetadata}"/>.</param>
		public void RegisterForm(Type formType)
		{
			if (this.registeredForms.TryGetValue(formType.GetFormId(), out var existingFormType))
			{
				if (formType != existingFormType)
				{
					throw new InvalidConfigurationException(
						$"Types '{formType.FullName}' and '{existingFormType.FullName}' have same form ID. Each form must have a unique form ID.");
				}
			}

			this.registeredForms.GetOrAdd(
				formType.GetFormId(),
				formType);
		}

		private FormInfo BuildFormMetadata(Type formType)
		{
			if (!formType.ImplementsClass(typeof(AsyncForm<,,>)))
			{
				throw new InvalidConfigurationException(
					$"Type '{formType.FullName}' was decorated with FormAttribute, but does not implement IForm<,,> or IAsyncForm<,,> interface.");
			}

			var iRequestHandlerInterface = formType.GetInterfaces(typeof(IRequestHandler<,>)).Single();

			var requestType = iRequestHandlerInterface.GenericTypeArguments[0];
			var responseType = iRequestHandlerInterface.GenericTypeArguments[1];

			var formMetadata = this.binder.BindForm(formType, requestType, responseType);

			return new FormInfo
			{
				FormType = formType,
				RequestType = requestType,
				ResponseType = responseType,
				Metadata = formMetadata
			};
		}
	}
}