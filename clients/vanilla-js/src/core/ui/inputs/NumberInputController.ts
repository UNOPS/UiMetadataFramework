import * as umf from "core-framework";

export class NumberInputController extends umf.InputController<number> {
	serializeValue(value: number | string): string {
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