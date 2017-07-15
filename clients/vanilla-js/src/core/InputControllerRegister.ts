import * as umf from "./ui-metadata-framework/index";
import { InputController, StringInputController } from "./InputController";

interface InputFieldControllerConstructor {
    new (metadata: umf.InputFieldMetadata): InputController<any>;
}

class InputControllerRegisterEntry {
    controller: InputFieldControllerConstructor;
    component: any;
}

export class InputControllerRegister {
    controllers: { [id: string]: InputControllerRegisterEntry } = {};

    createControllers(fields: umf.InputFieldMetadata[]) {
        var result = [];

        for (let field of fields) {
            // Instantiate new input controller.
            var entry = this.controllers[field.type] || <InputControllerRegisterEntry>{};
            let ctor = entry.controller || StringInputController;
            result.push(new ctor(field));
        }

        result.sort((a: InputController<any>, b: InputController<any>) => {
            return a.metadata.orderIndex - b.metadata.orderIndex;
        });

        return result;
    }

    register(name: string, svelteComponent: any, controller: InputFieldControllerConstructor) {
        this.controllers[name] = {
            controller: controller,
            component: svelteComponent
        };
    }
}