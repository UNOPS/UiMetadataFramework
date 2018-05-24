import {
	InputFieldEventHandler,
	InputController,
	FormInstance,
	FormEventArguments
} from "../../framework/index";
import * as umf from '../../framework/uimf-core/src/index';

export class InputLogToConsole extends InputFieldEventHandler {
	run(input: InputController<any>, eventHandlerMetadata: umf.EventHandlerMetadata, args: FormEventArguments): Promise<any> {
		return input.serialize().then(t => {
			console.log(`[${eventHandlerMetadata.runAt}] input event handler '${eventHandlerMetadata.id}' from '${input.metadata.id}'`);
		});
	}
}