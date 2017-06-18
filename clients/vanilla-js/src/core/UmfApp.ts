import { FormMetadata } from "./ui-metadata-framework/index";
import { UmfServer } from "./UmfServer";
import { FormInstance } from "./FormInstance";

export class UmfApp {
	forms: FormMetadata[];
	private readonly formsById: { [id: string]: FormMetadata } = {};
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