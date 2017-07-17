import * as umf from "core-framework";

export class BooleanInputController extends umf.InputController<boolean> {
	serializeValue(value: boolean | string): string {
		return value != null ? value.toString() : null;
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