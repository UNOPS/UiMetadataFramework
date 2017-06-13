import * as umf from "./ui-metadata-framework/index";
import { $ } from "./$";

export class UmfApp {
	getMetadata(formId: string): Promise<umf.FormMetadata> {
		return $.get(`http://localhost:62790/api/form/metadata/${formId}`).then((response:umf.FormMetadata) => {
			console.log(response);
			return response;
		}); 
	} 

	getAllMetadata(): Promise<umf.FormMetadata[]> {
		return $.get(`http://localhost:62790/api/form/metadata/`).then((response:umf.FormMetadata[]) => {
			console.log(response);
			return response;
		}); 
	}
}