import { FormResponse } from "uimf-core";
import { FormInstance } from "./FormInstance";

export interface IFormResponseHandler {
	readonly name: string;
	handle(response: FormResponse, form: FormInstance);
}