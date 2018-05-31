import { Component, Output, OnInit, Input } from "@angular/core";

@Component({
	selector: 'textValue-output',
	templateUrl: 'TextValue.html'
})
export class TextValue implements OnInit{
	appOutput: any;
	fieldOutput: any;

	@Input() app: any;
	@Input() field: any;

	ngOnInit() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}
}