import { Component, Input, OnInit } from "@angular/core";

@Component({
	selector: 'number-output',
	templateUrl: 'Number.html'
})
export class NumberOutput implements OnInit {
	@Input() app: any;
	@Input() field: any;

	appOutput: any;
	fieldOutput: any;

	ngOnInit() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}
}