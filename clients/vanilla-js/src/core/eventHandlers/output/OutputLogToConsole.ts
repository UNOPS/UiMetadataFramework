import {
	OutputFieldValue,
	OutputFieldEventHandler,
	FormEventArguments
} from "../../framework/index";
import { EventHandlerMetadata } from "uimf-core";

export class OutputLogToConsole extends OutputFieldEventHandler {
	run(output: OutputFieldValue, eventHandlerMetadata: EventHandlerMetadata, args: FormEventArguments): Promise<void> {
		console.log(`[${eventHandlerMetadata.runAt}] output event handler '${eventHandlerMetadata.id}' from '${output.metadata.id}'`);
		return Promise.resolve();
	}
}