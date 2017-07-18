namespace UiMetadataFramework.MediatR
{
	using System;
	using UiMetadataFramework.Core;

	/// <summary>
	/// Holds metadata information about a form.
	/// </summary>
	public class FormInfo
	{
		public Type FormType { get; set; }
		public FormMetadata Metadata { get; set; }
		public Type RequestType { get; set; }
		public Type ResponseType { get; set; }
	}
}