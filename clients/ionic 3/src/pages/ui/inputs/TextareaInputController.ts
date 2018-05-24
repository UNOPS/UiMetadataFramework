import * as umf from '../../../core/framework/index';
export class TextareaInputController extends umf.InputController<Textarea> {
	selected: string;

	serializeValue(value: Textarea | string): string {
		if (typeof (value) === 'string') {
			return value;
		}

		return value != null ? value.value : null;
	}

	init(value: string): Promise<TextareaInputController> {
		return new Promise((resolve, reject) => {
			this.selected = value;
			this.value = this.parse(value);
			resolve(this);
		});
	}

	getValue(): Promise<Textarea> {
		return Promise.resolve(this.parse(this.selected));
	}

	private parse(value: string): Textarea {
		return value == null || value === '' ? null : { value: value };
	}
}

class Textarea {
	value: string;
}