import * as umf from "uimf-core";
import { IFormResponseHandler, FormInstance } from "core-framework";

export class ReloadResponseHandler implements IFormResponseHandler {
	public readonly name: string = "reload";
	private readonly getFormUrl: (form: string, inputFieldValues: any) => Promise<string>;

	constructor(getFormUrl: (form: string, inputFieldValues: any) => Promise<string>) {
		this.getFormUrl = getFormUrl;
	}

	handle(response: ReloadResponse, form: FormInstance) {
		this.getFormUrl(response.form, response.inputFieldValues).then(url => {
			window.location.href = url;
		});
	}
}

class ReloadResponse extends umf.FormResponse {
	/**
	 * Gets or sets name of the form to redirect to.
	 */
	public form: string;

	/**
	 * Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
	 */
	public inputFieldValues: any;
}