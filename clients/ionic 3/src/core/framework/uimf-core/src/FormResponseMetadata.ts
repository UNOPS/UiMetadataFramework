import { ClientFunctionMetadata } from "./ClientFunctionMetadata";

/**
 * Metadata describing how to handle the response.
 */
export class FormResponseMetadata {
	/**
	 * Gets or sets name of the client-side handler which will be responsible for processing this <see cref="FormResponse"/>.
	 * @description Client can implement an arbitrary number of handlers, however usually it will at
		* least need to have an "object" handler, which will simply render the response. Other
		* handlers might include "redirect" handler, which will redirect to another form or URL.
	 */
	public handler: string;

	/**
	 * Gets or sets functions that the client should run immediately after it receives this response.
	 * These functions will be run before the response is handled by the `handler`.
	 */
	public functionsToRun: ClientFunctionMetadata[];
}