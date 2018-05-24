import * as umf from './uimf-core/src/index';
import {
    InputController,
    StringInputController,
    OutputFieldValue,
    InputFieldEventHandler,
    OutputFieldEventHandler,
    FormEventHandler,
    IFunctionRunner
} from '../framework/index';

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
    inputFieldEventHandlers: { [id: string]: InputFieldEventHandler } = {};
    outputFieldEventHandlers: { [id: string]: OutputFieldEventHandler } = {};
    formEventHandlers: { [id: string]: FormEventHandler } = {};
    functions: { [id: string]: IFunctionRunner } = {};

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

    getOutput(field: OutputFieldValue) {
        return field != null
            ? this.outputs[field.metadata.type] || this.outputs["text"]
            : this.outputs["text"];
    }

    getInput(type: string) {
        return type != null
            ? this.inputs[type] || this.inputs["text"]
            : this.inputs["text"];
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

    registerFormEventHandler(name: string, handler: FormEventHandler) {
        this.formEventHandlers[name] = handler;
    }

    registerInputFieldEventHandler(name: string, handler: InputFieldEventHandler) {
        this.inputFieldEventHandlers[name] = handler;
    }

    registerOutputFieldEventHandler(name: string, handler: OutputFieldEventHandler) {
        this.outputFieldEventHandlers[name] = handler;
    }

    registerFunction(name: string, fn: IFunctionRunner) {
        this.functions[name] = fn;
    }
}