import { FormResponse } from "./ui-metadata-framework/index";
import { FormInstance } from "./FormInstance";

export interface IFormResponseHandler {
	readonly name: string;
	handle(response: FormResponse, form: FormInstance);
}