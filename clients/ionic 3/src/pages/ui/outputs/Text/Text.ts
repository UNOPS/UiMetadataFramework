import { Component, Output } from "@angular/core";

@Component({
	selector: 'text-output',
	templateUrl: 'Text.html'
})
export class TextOutput {
	appOutput: any;
	fieldOutput: any;

	@Output() app: any;
	@Output() field: any;

	created() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}
}