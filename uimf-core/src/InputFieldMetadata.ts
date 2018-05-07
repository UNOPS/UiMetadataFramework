import { EventHandlerMetadata } from "./EventHandlerMetadata";

/**
 * Represents metadata for a single input field. * 
 */
export class InputFieldMetadata {
	constructor(metadata: any) {
		for (var property in metadata) {
			if (metadata.hasOwnProperty(property)) {
				this[property] = metadata[property];
			}
		}
	}

	/**
	 * Gets or sets additional parameters for the client control.
	 */
	public customProperties: any;

	/**
	 * Gets or sets event handlers for this input field.
	 */
	public eventHandlers: EventHandlerMetadata[];

	/**
	 * Gets or sets value indicating wheather value for this input field is required before submitting the form.
	 */
	public required: boolean;

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