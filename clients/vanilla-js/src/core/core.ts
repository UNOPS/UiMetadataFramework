import * as umf from "./ui-metadata-framework/index";
import { $ } from "./$";

export class UmfApp {
	getMetadata(formId: string): Promise<umf.FormMetadata> {
		return $.get(`/form/metadata/${formId}`).then((response:umf.FormMetadata) => {
			console.log(response);
			return response;
		}); 
	} 

	getAllMetadata(): umf.FormMetadata[] {
		return null;
	}
}