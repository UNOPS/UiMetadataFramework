namespace UiMetadataFramework.Core
{
	public enum ParameterValueLocation
	{
		/// <summary>
		/// Parameter value comes from the response object.
		/// </summary>
		Response,

		/// <summary>
		/// Parameter value comes from the request object of the parent form.
		/// </summary>
		Request,

		/// <summary>
		/// Parameter value comes from the form (metadata) parameters.
		/// </summary>
		Form,

		/// <summary>
		/// Parameter value comes from the parent context.
		/// </summary>
		Context,

		/// <summary>
		/// Gets parameters from the parent form.
		/// </summary>
		ParentForm
	}
}