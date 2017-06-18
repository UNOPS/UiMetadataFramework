import * as umf from "./ui-metadata-framework/index";

export class FormInstance {
    public readonly metadata: umf.FormMetadata;

    public inputFieldValues: { [id: string]: InputFieldValue } = {};

    constructor(metadata: umf.FormMetadata) {
        this.metadata = metadata;

        for (let field of metadata.inputFields) {
            this.inputFieldValues[field.id] = {
                metadata: field,
                data: null
            };
        }
    }

    parseResponse(response: umf.FormResponse): OutputFieldValue[] {
        var fields = Array<OutputFieldValue>();

        for (let field in response) {
            if (response.hasOwnProperty(field) && field != "responseHandler") {
                fields.push({
                    metadata: this.metadata.outputFields.find(t => t.id.toLowerCase() == field.toLowerCase()),
                    data: response[field]
                });
            }
        }

        fields.sort((a: OutputFieldValue, b: OutputFieldValue) => {
            return a.metadata.orderIndex - b.metadata.orderIndex;
        })

        return fields;
    }
}

export class InputFieldValue {
    public metadata: umf.InputFieldMetadata;
    public data: any;
}

export class OutputFieldValue {
    public metadata: umf.OutputFieldMetadata;
    public data: any;
}