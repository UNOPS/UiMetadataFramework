import * as umf from "../../../src/index";

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

	init(value: string): Promise<DateInputController> {
		return new Promise((resolve, reject) => {
			this.value = this.parseDate(value);
			this.valueAsText = this.asString(this.value);

			resolve(this);
		});
	}

	getValue(): Promise<Date> {
		var date = this.parseDate(this.valueAsText);
		return Promise.resolve(date);
	}

	private asString(date: Date): string {
		return date != null
			? `${date.getFullYear()}-${this.format2DecimalPlaces(date.getMonth() + 1)}-${this.format2DecimalPlaces(date.getDate())}`
			: null;
	}

	private parseDate(value:string):Date {
		let dateAsNumber = Date.parse(value);
		return isNaN(dateAsNumber) ? null : new Date(dateAsNumber);
	}

	private format2DecimalPlaces(n) {
		return ("0" + n).slice(-2);
	}
}