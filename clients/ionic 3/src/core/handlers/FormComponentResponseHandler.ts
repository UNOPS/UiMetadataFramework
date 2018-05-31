import * as umf from "uimf-core";
import { IFormResponseHandler, FormInstance } from '../framework/index';

export class FormComponentResponseHandler implements IFormResponseHandler {
	public readonly name: string = "default";

	handle(response: umf.FormResponse, form: FormInstance, args: any) {
		if (args != null && args.formComponent != null) {
			args.formComponent.renderResponse(response);
		}
	}
}