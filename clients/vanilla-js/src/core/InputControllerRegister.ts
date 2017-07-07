import * as umf from "./ui-metadata-framework/index";
import { InputController, StringInputController } from "./InputController";

export class InputControllerRegister {
	controllers: { [id: string]: { new (metadata:umf.InputFieldMetadata): InputController<any> } } = {};

	createControllers(fields:umf.InputFieldMetadata[]) {
        var result = [];

        for (let field of fields) {
			// Instantiate new input controller.
			let ctor = this.controllers[field.type] || StringInputController;
            result.push(new ctor(field));
        }

        result.sort((a: InputController<any>, b: InputController<any>) => {
            return a.metadata.orderIndex - b.metadata.orderIndex;
        });

		return result;
    }
}