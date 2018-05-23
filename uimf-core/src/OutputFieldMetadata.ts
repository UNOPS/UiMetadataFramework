import { EventHandlerMetadata } from "./EventHandlerMetadata";

/**
 * Represents metadata for a single output field.
 */
export class OutputFieldMetadata {
	constructor(metadata: any) {
		for (var property in metadata) {
			if (metadata.hasOwnProperty(property)) {
				this[property] = metadata[property];
			}
		}

		// Special case for "paginated-data", to ensure that each column is also
		// an instance of OutputFieldMetadata class, instead of a plain javascript object.
		if (this.customProperties != null && this.customProperties.columns != null) {
			for (let columnPropertyName in this.customProperties.columns) {
				// Convert column to OutputFieldMetadata instance.
				let metadataAsJsonObject = this.customProperties.columns[columnPropertyName];
				this.customProperties.columns[columnPropertyName] = new OutputFieldMetadata(metadataAsJsonObject);
			}
		}
	}

	/**
	 * Gets or sets additional parameters for the client control.
	 */
	public customProperties: any;

	/**
	 * Gets or sets event handlers for this output field.
	 */
	public eventHandlers: EventHandlerMetadata[];

	/**
	 * Gets or sets id of the field to which this metadata belongs.
	 */
	public id: string;

	/**
	 * Gets or sets label for the output field.
	 */
	public label: string;

	/**
	 * Gets name of the client control which will render this output field.
	 */
	public type: string;

	/**
	 * Gets or sets value indicating whether this field should be visible or not.
	 */
	public hidden: boolean;

	/**
	 * Gets or sets value which will dictate rendering position of this field in relationship to output fields within the same <see cref="FormResponse"/>.
	 */
	public orderIndex: number;

	/**
	 * Gets value of a custom property.
	 * @param name name of the custom property to get.
	 * @returns value of the custom property or null if the property is undefined.
	 */
	public getCustomProperty(name: string): any {
		if (this.customProperties != null && this.customProperties[name]) {
			return this.customProperties[name];
		}

		return null;
	}
}