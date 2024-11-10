namespace UiMetadataFramework.MediatR;

using System;
using UiMetadataFramework.Core.Binding;

/// <summary>
/// Extension methods for <see cref="MetadataBinder"/>.
/// </summary>
public static class MetadataBinderExtensions
{
	/// <summary>
	/// Gets form metadata for the specified form.
	/// </summary>
	/// <param name="binder"><see cref="MetadataBinder"/> instance.</param>
	/// <typeparam name="TForm">Type representing the form.</typeparam>
	/// <typeparam name="TRequest">Type representing request for the form. 
	/// <see cref="FormMetadata.InputFields"/> will be deduced from this class.</typeparam>
	/// <typeparam name="TResponse">Type representing response of the form. 
	/// <see cref="FormMetadata.OutputFields"/> will be deduced from this class.</typeparam>
	/// <returns><see cref="FormMetadata"/> instance.</returns>
	public static FormMetadata BuildForm<TForm, TRequest, TResponse>(this MetadataBinder binder)
	{
		return binder.BuildForm(typeof(TForm), typeof(TRequest), typeof(TResponse));
	}

	/// <summary>
	/// Gets form metadata for the specified form.
	/// </summary>
	/// <param name="binder"><see cref="MetadataBinder"/> instance.</param>
	/// <param name="formType"> name="TForm">Type representing the form.</param>
	/// <param name="requestType">Type representing request for the form. 
	/// <see cref="FormMetadata.InputFields"/> will be deduced from this class.</param>
	/// <param name="responseType">Type representing response of the form. 
	/// <see cref="FormMetadata.OutputFields"/> will be deduced from this class.</param>
	/// <returns><see cref="FormMetadata"/> instance.</returns>
	public static FormMetadata BuildForm(
		this MetadataBinder binder,
		Type formType,
		Type requestType,
		Type responseType)
	{
		return new FormMetadata(binder, formType, requestType, responseType);
	}
}