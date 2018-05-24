import { ClientFunctionMetadata } from "./ClientFunctionMetadata";

/**
 * Represents a function which can be run at a specific time during form's lifecycle.
 */
export class EventHandlerMetadata extends ClientFunctionMetadata {
    /**
     * Gets or sets event at which the function will run.
     */
	runAt: string;
}