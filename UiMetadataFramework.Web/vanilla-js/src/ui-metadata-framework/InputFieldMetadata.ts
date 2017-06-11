import { InputFieldSource } from "./InputFieldSource";

/**
 * Represents metadata for a single input field. * 
 */
export class InputFieldMetadata {
	/**
	 * Gets or sets additional parameters for the client control.
	 */
	public CustomProperties: any;

	/**
	 * Gets or sets source from which the default value for the input field will be taken. If null, then the field will not have a default value.
	 */
	public DefaultValue: InputFieldSource;

	/**
	 * Gets or sets value indicating wheather value for this input field is required before submitting the form.
	 */
	public Required: boolean;

	/**
	 * Gets or sets id of the field to which this metadata belongs.
	 */
	public Id: string;

	/**
	 * Gets or sets label for the output field.
	 */
	public Label: string;

	/**
	 * Gets name of the client control which will render this output field.
	 */
	public Type: string;

	/**
	 * Gets or sets value indicating whether this field should be visible or not.
	 */
	public Hidden: boolean;

	/**
	 * Gets or sets value which will dictate rendering position of this field in relationship to output fields within the same <see cref="FormResponse"/>.
	 */
	public OrderIndex: number;
}