import { InputFieldValue } from "./InputFieldValue";
import * as umf from "./ui-metadata-framework/index";

export abstract class InputController<T> {
	readonly metadata: umf.InputFieldMetadata;
	value: T;

	constructor(metadata: umf.InputFieldMetadata) {
		this.metadata = metadata;
	}

	abstract serialize(): Promise<{ value: string, input: InputController<T> }>;
	abstract init(value: string): Promise<InputController<T>>;
	abstract getValue(): Promise<T>;
}

export class StringInputController extends InputController<string> {
	constructor(metadata: umf.InputFieldMetadata) {
		super(metadata);
	}

	serialize(): Promise<{ value: string, input: StringInputController }> {
		return new Promise((resolve, reject) => {
			resolve({
				value: this.value != null ? this.value.toString() : null,
				input: this
			})
		});
	}

	init(value: string): Promise<StringInputController> {
		this.value = value;
		return Promise.resolve(this);
	}

	getValue(): Promise<string> {
		return Promise.resolve(this.value);
	}
}