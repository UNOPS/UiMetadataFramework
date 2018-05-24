import { Component, Input } from "@angular/core";

@Component({
	selector: 'number-output',
	templateUrl: 'Number.html'
})
export class NumberOutput  {
	@Input() app: any;
	@Input() field: any;

	appOutput: any;
	fieldOutput: any;

	created() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}
}