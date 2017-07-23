/**
 * Represents a function which can be run at a specific time during form's lifecycle to manipulate input field.
 */
export class InputFieldProcessorMetadata {
    /**
     * Gets or sets custom properties describing how to run the processor.
     */
    customProperties: any;

    /**
     * Gets or sets id of the processor.
     */
    id: string;

    /**
     * Gets or sets event at which the processor will run.
     */
    runAt: string;
}