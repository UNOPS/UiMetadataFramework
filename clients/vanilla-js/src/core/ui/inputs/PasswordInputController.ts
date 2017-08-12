import * as umf from "core-framework";

export class PasswordInputController extends umf.InputController<Password> {
	selected: string;

	serializeValue(value: Password | string): string {
		if (typeof (value) === "string") {
			return value;
		}

		return value != null ? value.value : null;
	}

	init(value: string): Promise<PasswordInputController> {
		return new Promise((resolve, reject) => {
			this.selected = value;
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<Password> {
		return Promise.resolve(this.parse(this.selected));
	}

	private parse(value: string): Password {
		return value == null || value == "" ? null : { value: value };
	}
}

class Password {
	value: string;
}