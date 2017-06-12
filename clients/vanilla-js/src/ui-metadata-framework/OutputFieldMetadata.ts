/**
 * Represents metadata for a single output field.
 */
export class OutputFieldMetadata {
	/**
	 * Gets or sets additional parameters for the client control.
	 */
	public CustomProperties: any;

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