import * as umf from "../../../src/core/index";

export class DateInputController extends umf.InputController<Date> {
	serialize(): Promise<{ value: string, input: DateInputController }> {
		var serialized = `${this.value.getFullYear()}-${this.format2DecimalPlaces(this.value.getMonth() + 1)}-${this.format2DecimalPlaces(this.value.getDate())}`;
		
		return new Promise((resolve, reject) => {
			resolve({
				value: serialized,
				input: this
			})
		});
	}

	init(value: string): Promise<DateInputController> {
		return new Promise((resolve, reject) => {
			this.value = new Date(value);
			resolve(this);
		});
	}

	getValue():Promise<Date> {
		return Promise.resolve(this.value);
	}

	private format2DecimalPlaces (n) {
        return ("0" + n).slice(-2);
    }
}