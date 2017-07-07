import * as umf from "../../../src/core/index";

export class NumberInputController extends umf.InputController<number> {
	serialize(): Promise<{ value: string, input: NumberInputController }> {
		return new Promise((resolve, reject) => {
			resolve({
				value: this.value != null ? this.value.toString() : null,
				input: this
			})
		});
	}

	init(value: string): Promise<NumberInputController> {
		return new Promise((resolve, reject) => {
			var v = parseFloat(value);
			this.value = isNaN(v) ? null : v;
			resolve(this);
		});
	}

	getValue(): Promise<number> {
		return Promise.resolve(this.value);
	}
}