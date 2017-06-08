namespace UiMetadataFramework.Core.Metadata
{
	using System.Collections.Generic;

	public class FormMetadata
	{
		public FormMetadata()
		{
		}

		public FormMetadata(FormMetadata form)
		{
			this.Inputs = form.Inputs;
			this.Outputs = form.Outputs;
			this.Type = form.Type;
		}

		/// <summary>
		/// Gets a list of inputs for the form.
		/// </summary>
		public IList<InputMetadata> Inputs { get; set; } = new List<InputMetadata>();

		/// <summary>
		/// Gets or sets output metadata for form's output object.
		/// </summary>
		public IList<PropertyMetadata> Outputs { get; set; } = new List<PropertyMetadata>();

		/// <summary>
		/// Gets or sets the type of form.
		/// </summary>
		public FormType Type { get; set; }
	}
}