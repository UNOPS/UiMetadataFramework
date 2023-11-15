namespace UiMetadataFramework.MediatR
{
	using System;
	using UiMetadataFramework.Core;

	/// <summary>
	/// Holds metadata information about a form.
	/// </summary>
	public class FormInfo
	{
		internal FormInfo()
		{
		}

		/// <summary>
		/// Gets or sets type of the form.
		/// </summary>
		public Type FormType { get; set; } = null!;

		/// <summary>
		/// Gets or sets metadata for the form.
		/// </summary>
		public FormMetadata Metadata { get; set; } = null!;

		/// <summary>
		/// Gets or sets type of the "request" class for the form.
		/// </summary>
		public Type RequestType { get; set; } = null!;

		/// <summary>
		/// Gets or sets type of the "response" class for the form.
		/// </summary>
		public Type ResponseType { get; set; } = null!;
	}
}