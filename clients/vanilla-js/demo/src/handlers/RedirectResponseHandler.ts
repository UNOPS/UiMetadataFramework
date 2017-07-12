import * as umf from "../../../src/index";

export class RedirectResponseHandler implements umf.IFormResponseHandler {
	public readonly name: string = "redirect";
	private readonly goToForm: (form: string, inputFieldValues: any) => void;

	constructor(goToForm: (form: string, inputFieldValues: any) => void) {
		this.goToForm = goToForm;
	}

	handle(response: RedirectResponse, form: umf.FormInstance) {
		this.goToForm(response.form, response.inputFieldValues);
	}
}

class RedirectResponse extends umf.FormResponse {
	/**
	 * Gets or sets name of the form to redirect to.
	 */
	public form: string;

	/**
	 * Gets or sets values for the input fields of the form (i.e. - <see cref="FormMetadata.InputFields"/>).
	 */
	public inputFieldValues: any;
}