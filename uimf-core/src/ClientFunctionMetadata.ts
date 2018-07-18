/**
 * Represents a function.
 */
export class ClientFunctionMetadata {
    constructor(metadata: any) {
		for (var property of Object.keys(metadata)) {
			this[property] = metadata[property];
		}
    }

    /**
     * Gets or sets custom properties describing how to run the function.
     */
    customProperties: any;

    /**
     * Gets or sets id of the function.
     */
    id: string;

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