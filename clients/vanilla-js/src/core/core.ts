import * as umf from "./ui-metadata-framework/index";
import { $ } from "./$";

export class UmfApp {
	getMetadata(formId: string): Promise<umf.FormMetadata> {
		return $.get(`http://localhost:62790/api/form/metadata/${formId}`).then((response: umf.FormMetadata) => {
			return response;
		}).catch(e => {
			console.warn(`Did not find form "${formId}".`)
			return null;
		});
	}

	getAllMetadata(): Promise<umf.FormMetadata[]> {
		return $.get(`http://localhost:62790/api/form/metadata/`).then((response: umf.FormMetadata[]) => {
			return response;
		});
	}
}