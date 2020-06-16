namespace UiMetadataFramework.Basic.Input.Typeahead
{
	using System;

	/// <summary>
	/// Identifies a request parameter of a remote source and instructs client how to set the value
	/// for this parameter. Each request to the remote source will have this parameter value set accordingly.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	public class RemoteSourceArgumentAttribute : Attribute
	{
		/// <summary>
		/// Creates a new instance of <see cref="RemoteSourceArgumentAttribute"/>.
		/// </summary>
		/// <param name="parameter">Name of the request parameter on the remote source.</param>
		/// <param name="source">Name of the source from which to take the value for the <paramref name="parameter"/>.</param>
		/// <param name="sourceType">Type of source specified by <paramref name="source"/>.</param>
		public RemoteSourceArgumentAttribute(string parameter, string source, string sourceType)
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

		/// <summary>
		/// Gets instance of the <see cref="RemoteSourceArgument"/> based on the configuration of this attribute.
		/// </summary>
		public RemoteSourceArgument GetArgument()
		{
			return new RemoteSourceArgument(this.Parameter, this.Source, this.SourceType);
		}
	}
}