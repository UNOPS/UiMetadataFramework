namespace UiMetadataFramework.Core
{
	/// <summary>
	/// Indicates where to get value for a certain form request parameter.
	/// </summary>
	public class FormParameter
	{
		/// <summary>
		/// Initializes a new instance of <see cref="FormParameter"/> class.
		/// </summary>
		/// <param name="parameter">Name of the request parameter.</param>
		/// <param name="property">Name of the property from which to get the value for the <see cref="Parameter"/>.</param>
		/// <param name="source">Indicates where the parameter value comes from.</param>
		/// <param name="target">Indicates where the parameter value is stored.</param>
		public FormParameter(
			string parameter,
			string property,
			ParameterValueLocation source = ParameterValueLocation.Response,
			ParameterValueLocation target = ParameterValueLocation.Form)
		{
			this.Property = property;
			this.Parameter = parameter;
			this.Source = source;
			this.Target = target;
		}

		/// <summary>
		/// Name of the request parameter.
		/// </summary>
		public string Parameter { get; }

		/// <summary>
		/// Name of the property form which to get value for the <see cref="Parameter"/>.
		/// </summary>
		public string Property { get; }

		/// <summary>
		/// Indicates where to get parameter value from (e.g. - request object or response object).
		/// </summary>
		public ParameterValueLocation Source { get; }

		/// <summary>
		/// Indicates parameter type.
		/// </summary>
		public ParameterValueLocation Target { get; }
	}
}