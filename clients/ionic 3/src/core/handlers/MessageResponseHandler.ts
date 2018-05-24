import * as umf from '../framework/uimf-core/src/index';
import { IFormResponseHandler, FormInstance } from '../framework/index';

export class MessageResponseHandler implements IFormResponseHandler {
	public readonly name: string = "message";

	handle(response: umf.FormResponse, form: FormInstance) {
		alert((<any>response).message);
	}
}