import { InputController } from "./InputController";
import { FormInstance } from "./FormInstance";
import { FormEventArguments } from "./FormEventArguments";
import { EventHandlerMetadata } from "uimf-core";

export abstract class InputFieldEventHandler {
	id: string;

	abstract run(input: InputController<any>, eventHandlerMetadata: EventHandlerMetadata, args: FormEventArguments): Promise<InputController<any>>;
}