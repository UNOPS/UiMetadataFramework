import { Component, Input, OnInit } from "@angular/core";

@Component({
	selector: 'formInstance-output',
	templateUrl: 'FormInstance.html'
})
export class FormInstance implements OnInit{
	fieldOutput: any;

	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;
	@Input() showLabel: any;

	ngOnInit() {
		this.fieldOutput = this.field;
	}
}