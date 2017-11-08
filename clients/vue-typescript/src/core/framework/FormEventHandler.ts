import { FormInstance } from "./FormInstance";
import { FormEventArguments } from "./FormEventArguments";
import { EventHandlerMetadata } from "uimf-core";

export abstract class FormEventHandler {
	id: string;

	abstract run(form: FormInstance, eventHandlerMetadata: EventHandlerMetadata, args: FormEventArguments): Promise<void>;
}