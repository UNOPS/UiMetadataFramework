import * as umf from "./ui-metadata-framework/index";
import { $ } from "./$";

export class UmfServer {
	private readonly getMetadataUrl: string;

	/**
	 * Creates a new instance of UmfApp.
	 */
	constructor(getMetadataUrl: string) {
		this.getMetadataUrl = getMetadataUrl;
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
}