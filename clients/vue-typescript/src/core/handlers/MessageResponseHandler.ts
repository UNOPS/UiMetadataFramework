import * as umf from "uimf-core";
import { IFormResponseHandler, FormInstance } from "core-framework";

export class MessageResponseHandler implements IFormResponseHandler {
	public readonly name: string = "message";

	handle(response: umf.FormResponse, form: FormInstance) {
		alert((<any>response).message);
	}
}