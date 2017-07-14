import * as umf from "./ui-metadata-framework/index";
import { InputController } from "./InputController";

export class InputFieldValue {
    public metadata: umf.InputFieldMetadata;
    public data: any;
    public serializedData: string;
    public controller: InputController<any>;
}