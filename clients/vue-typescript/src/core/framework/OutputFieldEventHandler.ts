import * as umf from "uimf-core";
import { OutputFieldValue } from "./OutputFieldValue";
import { FormEventArguments } from "./FormEventArguments";

export abstract class OutputFieldEventHandler {
	id: string;

	abstract run(output: OutputFieldValue, eventHandlerMetadata: umf.EventHandlerMetadata, args: FormEventArguments): Promise<void>;
}