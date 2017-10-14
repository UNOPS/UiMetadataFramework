import { ClientFunctionMetadata } from "uimf-core";

export interface IFunctionRunner {
	run(params: ClientFunctionMetadata): Promise<void>;
}