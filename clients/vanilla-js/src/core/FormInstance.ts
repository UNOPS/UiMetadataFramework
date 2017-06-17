import * as umf from "./ui-metadata-framework/index";

export class FormInstance {
    public readonly metadata: umf.FormMetadata;

    public inputFieldValues: { [id: string]: any } = {};

    constructor(metadata: umf.FormMetadata) {
        this.metadata = metadata;
    }
}