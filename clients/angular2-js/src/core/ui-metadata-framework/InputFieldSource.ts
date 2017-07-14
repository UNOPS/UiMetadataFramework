/**
 * Represents a source from which a value can be retrieved. This class can be useful for
 * binding input field values to a specific data source.
 */
export class InputFieldSource {
	/**
	 * Gets or sets id of the item within the source.
	 */
	public id: string;

	/**
	 * Gets or sets type of the source.
	 */
	public type: string;
}