import * as umf from "../../../src/core/index";

export class DateInputController extends umf.InputController<Date> {
	valueAsText: string = null;

	serialize(): Promise<{ value: string, input: DateInputController }> {
		return this.getValue().then(date => {
			return new Promise((resolve, reject) => {
				resolve({
					value: this.asString(date),
					input: this
				});
			});
		});
	}

	private asString(date: Date): string {
		return date != null
			? `${date.getFullYear()}-${this.format2DecimalPlaces(date.getMonth() + 1)}-${this.format2DecimalPlaces(date.getDate())}`
			: null;
	}

	init(value: string): Promise<DateInputController> {
		return new Promise((resolve, reject) => {
			this.value = new Date(value);
			this.valueAsText = this.asString(this.value);

			resolve(this);
		});
	}

	getValue(): Promise<Date> {
		let dateAsNumber = Date.parse(this.valueAsText);
		let date = isNaN(dateAsNumber) ? null : new Date(dateAsNumber);

		return Promise.resolve(date);
	}

	private format2DecimalPlaces(n) {
		return ("0" + n).slice(-2);
	}
}