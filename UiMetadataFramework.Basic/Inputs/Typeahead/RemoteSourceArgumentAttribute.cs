namespace UiMetadataFramework.Basic.Inputs.Typeahead
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.Binding;

	/// <summary>
	/// Identifies a request parameter of a remote source and instructs client how to set the value
	/// for this parameter. Each request to the remote source will have this parameter value set accordingly.
	/// </summary>
	public class RemoteSourceArgumentAttribute : ComponentConfigurationAttribute
	{
		/// <summary>
		/// Creates a new instance of <see cref="RemoteSourceArgumentAttribute"/>.
		/// </summary>
		/// <param name="parameter">Name of the request parameter on the remote source.</param>
		/// <param name="source">Name of the source from which to take the value for the <paramref name="parameter"/>.</param>
		/// <param name="sourceType">Type of source specified by <paramref name="source"/>.</param>
		public RemoteSourceArgumentAttribute(
			string parameter,
			string source,
			string sourceType)
		{
			this.Parameter = parameter;
			this.Source = source;
			this.SourceType = sourceType;
		}

		/// <summary>
		/// Gets or sets name of the request parameter on the remote source.
		/// </summary>
		[ConfigurationProperty("Parameter")]
		public string Parameter { get; set; }

		/// <summary>
		/// Gets or sets name of the source from which to take the value for the <see cref="Parameter"/>.
		/// </summary>
		[ConfigurationProperty("Source")]
		public string Source { get; set; }

		/// <summary>
		/// Gets or sets type of source specified in <see cref="Source"/>.
		/// </summary>
		[ConfigurationProperty("SourceType")]
		public string SourceType { get; set; }

		/// <summary>
		/// Builds an instance of <see cref="RemoteSourceArgumentAttribute"/> from a dictionary.
		/// </summary>
		public static RemoteSourceArgumentAttribute FromDictionary(Dictionary<string, object> t)
		{
			return new RemoteSourceArgumentAttribute(
				t[nameof(Parameter)].ToString(),
				t[nameof(Source)].ToString(),
				t[nameof(SourceType)].ToString());
		}
	}
}