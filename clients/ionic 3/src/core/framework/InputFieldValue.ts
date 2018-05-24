import * as umf from './uimf-core/src/index';
import { InputController } from "./InputController";

export class InputFieldValue {
    public metadata: umf.InputFieldMetadata;
    public data: any;
    public serializedData: string;
    public controller: InputController<any>;
}