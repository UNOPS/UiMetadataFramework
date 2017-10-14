import {
	InputFieldEventHandler,
	InputController,
	FormInstance,
	FormEventArguments
} from "../../framework/index";
import * as umf from "uimf-core";

export class BindToOutput extends InputFieldEventHandler {
	run(input: InputController<any>, eventHandler: umf.EventHandlerMetadata, args: FormEventArguments): Promise<any> {
		var promises = [];

		let lowercaseInputId = eventHandler.customProperties.outputFieldId.toLowerCase();
		
		for (let prop in args.response) {
			if (args.response.hasOwnProperty(prop) && prop.toLowerCase() === lowercaseInputId) {
				var serializedValue = input.serializeValue(args.response[prop]);				
				let promise = input.init(serializedValue);

				promises.push(promise);
				break;
			}
		}

		return Promise.all(promises);
	}
}