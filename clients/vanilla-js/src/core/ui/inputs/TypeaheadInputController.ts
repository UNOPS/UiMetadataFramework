import * as umf from "core-framework";

export interface ITypeaheadConfig {
	maxItemCount: number;
}

export class TypeaheadInputController 
	extends umf.InputController<TypeaheadValue> 
	implements ITypeaheadConfig 
{
	public maxItemCount = 1;

	serializeValue(value: TypeaheadValue | string): string {
		if (typeof (value) === "string") {
			return value;
		}

		return value != null ? value.value : null;
	}

	init(value: string): Promise<TypeaheadInputController> {
		return new Promise((resolve, reject) => {
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<TypeaheadValue> {
		return Promise.resolve(this.value);
	}

	private parse(value: string): TypeaheadValue {
		return value == null || value == "" 
			? new TypeaheadValue() 
			: new TypeaheadValue(value);
	}
}

class TypeaheadValue {
	constructor(value?) {
		this.value = value;
	}

	value:any;
}