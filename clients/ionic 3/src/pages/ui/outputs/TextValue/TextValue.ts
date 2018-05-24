import { Component, Output } from "@angular/core";

@Component({
	selector: 'textValue-output',
	templateUrl: 'TextValue.html'
})
export class TextValue {
	appOutput: any;
	fieldOutput: any;

	@Output() app: any;
	@Output() field: any;

	created() {
		this.app = this.app;
		this.field = this.field;
	}
}