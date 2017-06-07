namespace UiMetadataFramework.Core.UI.Inputs
{
	using System.Collections.Generic;

	public class FileUploadInput : Input
	{
		public FileUploadInput(string name, FormParameter parameter = null) : base(name, "fileUpload")
		{
			this.Parameter = parameter;
		}

		public List<string> AllowedExtensions { get; set; } = new List<string>();

		public bool AllowMultiple { get; set; } = false;

		public object Files { get; set; }

		/// <summary>
		/// Sets <see cref="AllowMultiple"/> value.
		/// </summary>
		public FormParameter Parameter { get; set; }
	}
}