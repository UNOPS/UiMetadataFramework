import { InputFieldValue } from "./InputFieldValue";
import * as umf from "uimf-core";

export abstract class InputController<T> {
	readonly metadata: umf.InputFieldMetadata;
	value: T;

	constructor(metadata: umf.InputFieldMetadata) {
		this.metadata = metadata;
	}

	abstract serializeValue(value: T | string): string;
	abstract init(value: string): Promise<InputController<T>>;
	abstract getValue(): Promise<T>;

	serialize(): Promise<{ value: string, input: InputController<T> }> {
		return this.getValue().then(t => {
			var valueAsString = this.serializeValue(t);
			return {
				value: valueAsString,
				input: this
			}
		});
	}
}

export class StringInputController extends InputController<string> {
	constructor(metadata: umf.InputFieldMetadata) {
		super(metadata);
	}

	serializeValue(value: string): string {
		// Ensure we don't return "undefined", but return null instead.
		return value != null ? value.toString() : null;
	}

	init(value: string): Promise<StringInputController> {
		this.value = value;
		return Promise.resolve(this);
	}

	getValue(): Promise<string> {
		return Promise.resolve(this.value);
	}
}