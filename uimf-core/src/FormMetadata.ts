import { InputFieldMetadata } from "./InputFieldMetadata";
import { OutputFieldMetadata } from "./OutputFieldMetadata";
import { EventHandlerMetadata } from "./EventHandlerMetadata";

/**
 * Encapsulates all information needed to render a form.
 */
export class FormMetadata {
	constructor(metadata: any) {
		for (var property in metadata) {
			if (metadata.hasOwnProperty(property)) {
				this[property] = metadata[property];
			}
		}

		this.outputFields = metadata.outputFields.map(t => new OutputFieldMetadata(t));
	}

	/**
	 * Gets or sets id of the form, to which this metadata belongs.
	 */
	public id: string;

	/**
	 * Gets or sets list of input fields.
	 */
	public inputFields: InputFieldMetadata[];

	/**
	 * Gets or sets label for this form.
	 */
	public label: string;

	/**
	 * Gets or sets list of output fields.
	 */
	public outputFields: OutputFieldMetadata[];

	/**
	 * Gets or sets a value indicating whether the form should be auto-posted as soon as it has been loaded by the client. This can be useful for displaying reports and to generally show data without user having to post the form manually. 
	 */
	public postOnLoad: boolean;

	/**
	 * Gets or sets value indicating whether the initial post (<see cref="PostOnLoad"/>) should validate all input fields before posting.
	 */
	public postOnLoadValidation: boolean;

	/**
	 * Gets or sets additional parameters for the client.
	 */
	public customProperties: any;

	/**
	 * Gets or sets event handlers for this form.
	 */
	public eventHandlers: EventHandlerMetadata[];

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