namespace UiMetadataFramework.Basic.Inputs.Typeahead
{
	/// <summary>
	/// Identifies a request parameter of a remote source and instructs client how to set the value
	/// for this parameter. Each request to the remote source will have this parameter value set accordingly.
	/// </summary>
	public class RemoteSourceArgument
	{
		/// <summary>
		/// Creates a new instance of <see cref="RemoteSourceArgumentAttribute"/>.
		/// </summary>
		/// <param name="parameter">Name of the request parameter on the remote source.</param>
		/// <param name="source">Name of the source from which to take the value for the <paramref name="parameter"/>.</param>
		/// <param name="sourceType">Type of the source.</param>
		public RemoteSourceArgument(string parameter, string source, string sourceType)
		{
			this.Parameter = parameter;
			this.Source = source;
			this.SourceType = sourceType;
		}

		/// <summary>
		/// Gets or sets name of the request parameter on the remote source.
		/// </summary>
		public string Parameter { get; set; }

		/// <summary>
		/// Gets or sets name of the source from which to take the value for the <see cref="Parameter"/>.
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		/// Gets or sets type of source specified in <see cref="Source"/>.
		/// </summary>
		public string SourceType { get; set; }
	}
}