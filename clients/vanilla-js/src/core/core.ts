import * as umf from "./ui-metadata-framework/index";
import { $ } from "./$";
import { FormInstance } from "./FormInstance";

export class UmfServer {
	private readonly getMetadataUrl: string;
	private readonly postFormUrl: string;

	/**
	 * Creates a new instance of UmfApp.
	 */
	constructor(getMetadataUrl: string, postFormUrl: string) {
		this.getMetadataUrl = getMetadataUrl;
		this.postFormUrl = postFormUrl;
	}

	getMetadata(formId: string): Promise<umf.FormMetadata> {
		return $.get(`${this.getMetadataUrl}/${formId}`).then((response: string) => {
			return <umf.FormMetadata>JSON.parse(response);
		}).catch(e => {
			console.warn(`Did not find form "${formId}".`)
			return null;
		});
	}

	getAllMetadata(): Promise<umf.FormMetadata[]> {
		return $.get(this.getMetadataUrl).then((response: string) => {
			return <umf.FormMetadata[]>JSON.parse(response);
		});
	}

	postForm(formInstance: FormInstance) {
		return $.post(this.postFormUrl, [{
			requestId: 1,
			form: formInstance.metadata.id,
			inputFieldValues: formInstance.inputFieldValues,
		}]);
	}
}

export class UmfApp {
	forms: umf.FormMetadata[];
	private readonly formsById: { [id: string]: umf.FormMetadata } = {};
	private readonly server: UmfServer;

	constructor(server: UmfServer) {
		this.server = server;
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
}

export * from "./ui-metadata-framework/index";