import { ClientFunctionMetadata } from './uimf-core/src/index';

export interface IFunctionRunner {
	run(params: ClientFunctionMetadata): Promise<void>;
}