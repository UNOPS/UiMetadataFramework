import * as umf from "core-framework";

export class DropdownInputController extends umf.InputController<DropdownValue> {
	selected: string;

	serializeValue(value: DropdownValue | string): string {
		if (typeof (value) === "string") {
			return value;
		}

		return value != null ? value.value : null;
	}

	init(value: string): Promise<DropdownInputController> {
		return new Promise((resolve, reject) => {
			this.selected = value;
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<DropdownValue> {
		return Promise.resolve(this.parse(this.selected));
	}

	private parse(value: string): DropdownValue {
		return value == null || value == "" ? null : { value: value };
	}
}

class DropdownValue {
	value: string;
}