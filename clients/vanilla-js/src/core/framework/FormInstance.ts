import * as umf from "uimf-core";
import { InputFieldValue } from "./InputFieldValue";
import { OutputFieldValue } from "./OutputFieldValue";
import { InputController } from "./InputController";
import { ControlRegister } from "./ControlRegister";

export class FormInstance {
    public readonly metadata: umf.FormMetadata;
    public outputs: Array<OutputFieldValue> = [];
    public inputs: Array<InputController<any>> = [];

    constructor(metadata: umf.FormMetadata, controllerRegister: ControlRegister) {
        this.metadata = metadata;
        this.inputs = controllerRegister.createInputControllers(this.metadata.inputFields);
    }

    initializeInputFields(data: any) {
        var promises = [];

        for (let fieldMetadata of this.inputs) {
            let value = null;

            if (data != null) {
                for (let prop in data) {
                    if (data.hasOwnProperty(prop) && prop.toLowerCase() == fieldMetadata.metadata.id.toLowerCase()) {
                        value = data[prop];
                        break;
                    }
                }
            }

            promises.push(fieldMetadata.init(value));
        }

        return Promise.all(promises);
    }

    setInputFields(data: any) {
        for (let field of this.inputs) {
            field.value = data[field.metadata.id];
        }
    }

    prepareForm(mustHaveAllRequiredInputs: boolean): any {
        var data = {};
        var promises = [];
        var hasRequiredMissingInput = false;

        for (let input of this.inputs) {
            var promise = input.getValue().then(value => {
                data[input.metadata.id] = value;

                if (input.metadata.required && (value == null || (typeof (value) === "string" && value == ""))) {
                    hasRequiredMissingInput = true;
                }
            });

            promises.push(promise);
        }

        return Promise.all(promises).then(() => {
            // If not all required inputs were entered, then do not post.
            if (hasRequiredMissingInput &&
                mustHaveAllRequiredInputs) {
                return null;
            }

            return data;
        });
    }

    getSerializedInputValues(): Promise<any> {
        var data = {};
        var promises = [];

        for (let input of this.inputs) {
            var promise = input.serialize().then(t => {
                // Don't include inputs without values, because we only
                // want to serialize "non-default" values.
                if (t.value != null && t.value != "") {
                    data[input.metadata.id] = t.value;
                }
            });

            promises.push(promise);
        }

        return Promise.all(promises).then(() => data);
    }

    getSerializedInputValuesFromObject(value: any): any {
        var data = {};

        var normalizedObject = {};
        for (let prop in value) {
            if (value.hasOwnProperty(prop)) {
                normalizedObject[prop.toLowerCase()] = value[prop];
            }
        }

        for (let input of this.inputs) {
            var valueAsString = input.serializeValue(normalizedObject[input.metadata.id.toLowerCase()]);

            // Don't include inputs without values, because we only
            // want to serialize "non-default" values.
            if (valueAsString != null && valueAsString != "") {
                data[input.metadata.id] = valueAsString;
            }
        }

        return data;
    }

    setOutputFieldValues(response: umf.FormResponse) {
        var fields = Array<OutputFieldValue>();

        var normalizedResponse = this.getNormalizedObject(response);

        for (let field of this.metadata.outputFields) {
            fields.push({
                metadata: field,
                data: normalizedResponse[field.id.toLowerCase()]
            });
        }

        fields.sort((a: OutputFieldValue, b: OutputFieldValue) => {
            return a.metadata.orderIndex - b.metadata.orderIndex;
        });

        this.outputs = fields;
    }

    runInputFieldProcessors(event: string, params: any, controlRegister: ControlRegister): Promise<any> {
        var promises = [];

        for (let input of this.inputs) {
            if (input.metadata.processors != null) {
                for (let processorMetadata of input.metadata.processors) {
                    var processor = controlRegister.inputProcessors[processorMetadata.id];
                    if (processor != null) {
                        var promise = processor.process(input, processorMetadata, params);
                        promises.push(promise);
                    }
                }
            }
        }

        return Promise.all(promises);
    }

    hasInputFieldProcessorsAt(runAt: string): boolean {
        for (let input of this.inputs) {
            var processor = input.metadata.processors.find(t => t.runAt === runAt);
            if (processor != null) {
                return true;
            }
        }

        return false;
    }

    private getNormalizedObject(response: umf.FormResponse): any {
        var normalizedResponse = {};
        for (let field in response) {
            if (response.hasOwnProperty(field) && field !== "metadata") {
                normalizedResponse[field.toLowerCase()] = response[field];
            }
        }

        return normalizedResponse;
    }
}