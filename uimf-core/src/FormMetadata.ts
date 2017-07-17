import { InputFieldMetadata } from "./InputFieldMetadata";
import { OutputFieldMetadata } from "./OutputFieldMetadata";

/**
 * Encapsulates all information needed to render a form.
 */
export class FormMetadata {
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
}