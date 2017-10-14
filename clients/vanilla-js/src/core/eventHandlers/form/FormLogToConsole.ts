import {
	FormInstance,
	FormEventHandler,
	FormEventArguments
} from "../../framework/index";
import { EventHandlerMetadata } from "uimf-core";

export class FormLogToConsole extends FormEventHandler {
	run(form: FormInstance, eventHandlerMetadata: EventHandlerMetadata, args: FormEventArguments): Promise<void> {
		console.log(`[${eventHandlerMetadata.runAt}] form event handler '${eventHandlerMetadata.id}' from '${form.metadata.id}'`);
		return Promise.resolve();
	}
}