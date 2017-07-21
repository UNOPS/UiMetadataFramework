import * as umf from "../../framework/index";

class PaginationParameters {
	constructor(pageIndex?: number | string, pageSize?: number | string, orderBy?: string, ascending?: boolean | string) {
		this.pageIndex = PaginationParameters.asInt(pageIndex, 1);
		this.pageSize = PaginationParameters.asInt(pageSize, 10);
		this.orderBy = orderBy || null;
		this.ascending = PaginationParameters.asBool(ascending, null);
	}

	pageSize: number;
	pageIndex: number;
	ascending: boolean;
	orderBy: string;

	private static asInt(value: number | string, defaultValue: number): number {
		if (typeof (value) === "string") {
			var result = parseInt(value);
			return isNaN(result) ? defaultValue : result;
		}

		if (value == null) {
			return defaultValue;
		}

		return value;
	}

	private static asBool(value: boolean | string, defaultValue: boolean): boolean {
		if (typeof (value) === "string" || value == null) {
			return value != null
				? value.toString() === "true"
				: defaultValue;
		}

		return value;
	}
}

export class PaginatorInputController extends umf.InputController<PaginationParameters> {
	serializeValue(value: PaginationParameters | string): string {
		var p = typeof (value) === "string" || value == null
			? this.parse(<string>value)
			: value;

		if (p.pageIndex == 1 &&
			p.pageSize == 10 &&
			p.ascending == null &&
			p.orderBy == null) {
			return "";
		}

		var result = `${p.pageIndex}-${p.pageSize}`;

		if (p.orderBy != null) {
			result += `-${p.ascending}-${p.orderBy}}`;
		}

		return result;
	}

	init(value: string): Promise<PaginatorInputController> {
		return new Promise((resolve, reject) => {
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<PaginationParameters> {
		return Promise.resolve(this.value);
	}

	private parse(value: string): PaginationParameters {
		// 1-10-asc-firstname
		// 1-10

		if (value == null || value.length === 0) {
			return new PaginationParameters();
		}

		var components = value.split("-");
		return new PaginationParameters(
			components[0],
			components[1],
			components[2],
			components[3]);
	}
}