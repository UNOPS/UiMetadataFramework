import * as umf from "./ui-metadata-framework/index";
import { OutputFieldValue } from "./index";
import { InputController, StringInputController } from "./InputController";

interface InputFieldControllerConstructor {
    new(metadata: umf.InputFieldMetadata): InputController<any>;
}

class InputControlEntry {
    controller: InputFieldControllerConstructor;
    component: any;
}

export class ControlRegister {
    inputs: { [id: string]: InputControlEntry } = {};
    outputs: { [id: string]: any } = {};

    createInputControllers(fields: umf.InputFieldMetadata[]) {
        var result = [];

        for (let field of fields) {
            // Instantiate new input controller.
            var entry = this.inputs[field.type] || <InputControlEntry>{};
            let ctor = entry.controller || StringInputController;
            result.push(new ctor(field));
        }

        result.sort((a: InputController<any>, b: InputController<any>) => {
            return a.metadata.orderIndex - b.metadata.orderIndex;
        });

        return result;
    }

    getOutput(field:OutputFieldValue) {
        return field != null 
            ? this.outputs[field.metadata.type] || this.outputs["text"]
            : this.outputs["text"];
    }

    registerInputFieldControl(name: string, svelteComponent: any, controller: InputFieldControllerConstructor) {
        this.inputs[name] = {
            controller: controller,
            component: svelteComponent
        };
    }

    registerOutputFieldControl(name: string, control: any, constants: any = null) {
        this.outputs[name] = {
            constructor: control,
            constants: constants
        };
    }
}