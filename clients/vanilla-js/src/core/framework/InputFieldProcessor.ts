import { InputController } from "./InputController";
import { FormInstance } from "./FormInstance";

export abstract class InputFieldProcessor {
	id: string;

	abstract process(input: InputController<any>, form: FormInstance, params: any): Promise<InputController<any>>;
}