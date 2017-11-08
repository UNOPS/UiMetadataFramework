import * as umf from "core-framework";

export class BooleanInputController extends umf.InputController<boolean> {
	serializeValue(value: boolean | string): string {
		var parsed = this.parse(value);
		return parsed != null ? parsed.toString() : null;
	}

	init(value: string): Promise<BooleanInputController> {
		return new Promise((resolve, reject) => {
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<boolean> {
		return Promise.resolve(this.parse(this.value));
	}

	private parse(value): boolean {
		return value != null
			? value.toString() == "true"
			: this.metadata.required ? false : null;
	}
}