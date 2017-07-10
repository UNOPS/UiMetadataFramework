import * as umf from "./ui-metadata-framework/index";
import { InputFieldValue } from "./InputFieldValue";
import { OutputFieldValue } from "./OutputFieldValue";
import { InputController } from "./InputController";
import { InputControllerRegister } from "./InputControllerRegister";

export class FormInstance {
    public readonly metadata: umf.FormMetadata;
    public outputFieldValues: Array<OutputFieldValue> = [];
    public inputFieldValues: Array<InputController<any>> = [];

    constructor(metadata: umf.FormMetadata, inputControllerRegister:InputControllerRegister) {
        this.metadata = metadata;
        this.inputFieldValues = inputControllerRegister.createControllers(this.metadata.inputFields);
    }

    initializeInputFields(data: any) {
        var promises = [];

        for (let fieldMetadata of this.inputFieldValues) {
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

    prepareForm():any {
        var data = {};
		var promises = [];
		var hasRequiredMissingInput = false;

		for (let input of this.inputFieldValues) {
			var promise = input.getValue().then(value => {
				data[input.metadata.id] = value;

				if (input.metadata.required && (value == null || value == "")) {
					hasRequiredMissingInput = true;
				}
			});

			promises.push(promise);
		}

		return Promise.all(promises).then(() => {
			// If not all required inputs were entered, then do not post.
			if (hasRequiredMissingInput) {
				return null;
			}

            return data;
        });
    }

    getSerializedInputValues():any {
        var data = {};
        var promises = [];
        
        for (let input of this.inputFieldValues) {
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

        this.outputFieldValues = fields;
    }

    private getNormalizedObject(response: umf.FormResponse): any {
        var normalizedResponse = {};
        for (let field in response) {
            if (response.hasOwnProperty(field) && field !== "responseHandler") {
                normalizedResponse[field.toLowerCase()] = response[field];
            }
        }

        return normalizedResponse;
    }
}