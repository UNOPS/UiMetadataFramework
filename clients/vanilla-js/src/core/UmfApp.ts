import { FormMetadata, FormResponse, FormResponseMetadata } from "./ui-metadata-framework/index";
import { UmfServer } from "./UmfServer";
import { FormInstance } from "./FormInstance";
import { IFormResponseHandler } from "./IFormResponseHandler";
import { InputFieldValue } from "./InputFieldValue";
import { InputControllerRegister } from "./InputControllerRegister";
import { IAppRouter } from "./IAppRouter";

export class UmfApp {
	forms: FormMetadata[];
	private readonly formsById: { [id: string]: FormMetadata } = {};
	public readonly server: UmfServer;
	public readonly formResponseHandlers: { [id: string]: IFormResponseHandler } = {};
	public inputControllerRegister: InputControllerRegister;
	public router: IAppRouter;

	constructor(server: UmfServer, inputRegister: InputControllerRegister) {
		this.server = server;
		this.inputControllerRegister = inputRegister;
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

	getFormInstance(formId: string, throwError: boolean = false) {
		let metadata = this.getForm(formId);

		if (metadata == null) {
			if (throwError) {
				throw Error(`Form ${formId} not found.`);
			}

			return null;
		}

		return new FormInstance(metadata, this.inputControllerRegister);
	}

	handleResponse(response: FormResponse, form: FormInstance) {
		var responseMetadata = response.metadata || new FormResponseMetadata();
		var handler = this.formResponseHandlers[responseMetadata.handler];

		if (handler == null) {
			throw new Error(`Cannot find FormResponseHandler "${responseMetadata.handler}".`);
		}

		return handler.handle(response, form);
	}
}

export * from "./ui-metadata-framework/index";