import * as umf from '../../../core/framework/index';
import * as axiosLib from 'axios';

let axios = axiosLib.default;

export class DynamicFormInputController extends umf.InputController<DynamicFormValue> {
	inputs: Array<umf.InputController<any>> = [];

	serializeValue(value: DynamicFormValue | string): any {
		let parsed = this.parse(value);
		return JSON.stringify(parsed);
	}

	init(value: any): Promise<DynamicFormInputController> {
		return new Promise((resolve, reject) => {
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<DynamicFormValue> {
		let value = this.parse(this.value);
		let promises = [];

		for (let input of value.inputs) {
			let controller = this.inputs.find(t => t.metadata.id === input.id);

			let p = controller.serialize().then(t => {
				input.value = t.value;
			});

			promises.push(p);
		}

		return Promise.all(promises).then(() => {
			return value;
		});
	}

	private parse(value: DynamicFormValue | string): DynamicFormValue {
		if (value == null) {
			return new DynamicFormValue();
		}

		let parsed: any;
		parsed = typeof (value) === 'string'
			? JSON.parse(value)
			: parsed = value;

		return parsed == null || typeof (parsed.inputs) === 'undefined'
			? new DynamicFormValue()
			: parsed;
	}
}

class DynamicFormValue {
	constructor(inputs: InputItem[] = []) {
		this.inputs = inputs;
	}

	inputs: InputItem[] = [];
}

class InputItem {
	public FormId: string;
	public required: boolean;
	public id: string;
	public label: string;
	public type: string;
	public orderIndex: number;
	public value: string;
}