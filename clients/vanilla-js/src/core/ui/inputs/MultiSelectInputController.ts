import * as umf from "core-framework";

export class MultiSelectInputController extends umf.InputController<MultiSelectValue> {
	serializeValue(value: MultiSelectValue | string): string {
		if (typeof (value) === "string") {
			return value;
		}

		return value != null ? (value.items || []).join(",") : null;
	}

	init(value: string): Promise<MultiSelectInputController> {
		return new Promise((resolve, reject) => {
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<MultiSelectValue> {
		return Promise.resolve(this.value);
	}

	private parse(value: string): MultiSelectValue {
		return value == null || value == "" 
			? new MultiSelectValue() 
			: new MultiSelectValue(value.split(","));
	}
}

class MultiSelectValue {
	constructor(items?:any[]) {
		this.items = items;
	}

	items:any[] = [];
}