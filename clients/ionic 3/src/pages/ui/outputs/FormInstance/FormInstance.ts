import { Component, Input } from "@angular/core";

@Component({
	selector: 'formInstance-output',
	templateUrl: 'FormInstance.html'
})
export class FormInstance {
	fieldOutput: any;

	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;
	@Input() showLabel: any;

	created() {
		this.fieldOutput = this.field;
	}
}