import * as umf from "../../../src/index";

export class MessageResponseHandler implements umf.IFormResponseHandler {
	public readonly name: string = "message";

	handle(response: umf.FormResponse, form: umf.FormInstance) {
		alert((<any>response).message);
	}
}