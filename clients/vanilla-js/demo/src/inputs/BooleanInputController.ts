import * as umf from "../../../src/core/index";

export class BooleanInputController extends umf.InputController<boolean> {
	serialize(): Promise<{ value: string, input: BooleanInputController }> {
		return new Promise((resolve, reject) => {
			resolve({
				value: this.value != null ? this.value.toString() : null,
				input: this
			})
		});
	}

	init(value: string): Promise<BooleanInputController> {
		return new Promise((resolve, reject) => {
			this.value = value != null 
				? value.toString() == "true" 
				: this.metadata.required ? false : null;
			resolve(this);
		});
	}

	getValue(): Promise<boolean> {
		return Promise.resolve(this.value);
	}
}