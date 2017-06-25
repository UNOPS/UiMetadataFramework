import * as umf from "../../../src/core/index";

export class RedirectResponseHandler implements umf.IFormResponseHandler {
	public readonly name: string = "redirect";
	private readonly stateRouter: any;

	constructor(stateRouter: any) {
		//debugger;
		this.stateRouter = stateRouter;
	}

	handle(response: RedirectResponse, form: umf.FormInstance) {
		this.stateRouter(response.form);
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