namespace UiMetadataFramework.Basic.Inputs;

using System;
using System.Collections.Generic;
using UiMetadataFramework.Core;
using UiMetadataFramework.Core.Binding;

/// <inheritdoc />
public class MyInputComponentAttribute(string name, Type? metadataFactory = null) : InputComponentAttribute(name, metadataFactory)
{
	/// <summary>
	/// Gets or sets value indicating whether input should never be explicitly rendered on the client.
	/// If this value is set to true, then <see cref="InputFieldMetadata.Hidden"/> will always
	/// be true.
	/// </summary>
	public bool AlwaysHidden { get; set; }

	/// <summary>
	/// Default label for the input.
	/// </summary>
	public string? DefaultLabel { get; set; }

	/// <inheritdoc />
	public override IReadOnlyDictionary<string, object?> GetAdditionalData()
	{
		return new Dictionary<string, object?>
		{
			{ nameof(this.AlwaysHidden), this.AlwaysHidden },
			{ nameof(this.DefaultLabel), this.DefaultLabel }
		};
	}
}