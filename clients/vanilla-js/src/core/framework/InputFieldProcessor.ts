import { InputController } from "./InputController";
import { FormInstance } from "./FormInstance";
import { InputFieldProcessorMetadata } from "uimf-core";

export abstract class InputFieldProcessor {
	id: string;

	abstract process(input: InputController<any>, processorMetadata: InputFieldProcessorMetadata, params: any): Promise<InputController<any>>;
}