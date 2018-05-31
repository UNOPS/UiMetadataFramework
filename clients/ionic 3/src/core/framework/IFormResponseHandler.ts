import { FormResponse } from './uimf-core/src/index';
import { FormInstance } from "./FormInstance";

export interface IFormResponseHandler {
	readonly name: string;
	handle(response: FormResponse, form: FormInstance, args: any);
}