import { FormResponseMetadata } from "./FormResponseMetadata";

/**
 * Represents response of a form.
 */
export class FormResponse extends Object {
	/**
	 * Represents response which has additional metadata describing how to render the results.
	 */
	metadata: FormResponseMetadata;
}