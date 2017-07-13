import * as umf from "../../../src/index";

export class NumberInputController extends umf.InputController<number> {
	serializeValue(value: number): string {
		return value != null ? value.toString() : null
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