import { FormMetadata, FormResponse } from "./ui-metadata-framework/index";
import { UmfServer } from "./UmfServer";
import { FormInstance, InputFieldValue } from "./FormInstance";
import { IFormResponseHandler } from "./IFormResponseHandler";

export class UmfApp {
	forms: FormMetadata[];
	private readonly formsById: { [id: string]: FormMetadata } = {};
	public readonly server: UmfServer;
	public readonly formResponseHandlers: { [id: string]: IFormResponseHandler } = {};
	public go: (form:string,inputFieldValues:InputFieldValue[]) => void;
	
	constructor(server: UmfServer) {
		this.server = server;
	}

	registerResponseHandler(handler: IFormResponseHandler) {
		this.formResponseHandlers[handler.name] = handler;
	}

	load() {
		return this.server.getAllMetadata()
			.then(response => {
				this.forms = response;

				for (let form of this.forms) {
					this.formsById[form.id] = form;
				}
			});
	}

	getForm(id: string) {
		return this.formsById[id];
	}

	postForm(formInstance: FormInstance) {
		return this.server.postForm(formInstance);
	}

	handleResponse(response: FormResponse, form: FormInstance) {
		var handler = this.formResponseHandlers[response.responseHandler];

		if (handler == null) {
			throw new Error(`Cannot find FormResponseHandler "${response.responseHandler}".`);
		}

		return handler.handle(response, form);
	}
}

export * from "./ui-metadata-framework/index";