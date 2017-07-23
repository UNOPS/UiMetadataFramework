import { InputFieldProcessor, InputController, FormInstance } from "../../framework/index";
import * as umf from "uimf-core";

export class BindToOutputProcessor extends InputFieldProcessor {
	process(input: InputController<any>, form: FormInstance, response: umf.FormResponse): Promise<any> {
		var promises = [];

		let lowercaseInputId = input.metadata.id.toLowerCase();

		for (let prop in response) {
			if (response.hasOwnProperty(prop) && prop.toLowerCase() === lowercaseInputId) {
				var serializedValue = input.serializeValue(response[prop]);
				let promise = input.init(serializedValue);

				promises.push(promise);
			}
		}

		return Promise.all(promises);
	}
}