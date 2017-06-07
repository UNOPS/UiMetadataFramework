namespace UiMetadataFramework.Core.Metadata
{
	using System.Collections.Generic;
	using UiMetadataFramework.Core.ResponseActions;
	using UiMetadataFramework.Core.UI.Inputs;

	public class FormMetadata : IHasPropertyMetadata
	{
		public FormMetadata()
		{
		}

		public FormMetadata(FormMetadata form)
		{
			this.Inputs = form.Inputs;
			this.Actions = form.Actions;
			this.Outputs = form.Outputs;
			this.Type = form.Type;
			this.Parameters = form.Parameters;
			this.ResponseAction = form.ResponseAction;
		}

		/// <summary>
		/// Gets a list of actions available on the form.
		/// </summary>
		public IList<PropertyMetadata> Actions { get; } = new List<PropertyMetadata>();

		/// <summary>
		/// Gets a list of inputs for the form.
		/// </summary>
		public IList<Input> Inputs { get; set; } = new List<Input>();

		/// <summary>
		/// Gets or sets form parameters, required to open the form in the UI.
		/// </summary>
		public object Parameters { get; set; }

		/// <summary>
		/// Gets or sets <see cref="ResponseAction"/> object, which will specify what the client
		/// should do once it receives response from the server. If null, the default
		/// action will be taken (the response data will be displayed to client).
		/// </summary>
		public ResponseAction ResponseAction { get; set; }

		/// <summary>
		/// Gets or sets the type of form.
		/// </summary>
		public FormType Type { get; set; }

		/// <summary>
		/// Gets or sets output metadata for form's output object.
		/// </summary>
		public IList<PropertyMetadata> Outputs { get; set; } = new List<PropertyMetadata>();
	}
}