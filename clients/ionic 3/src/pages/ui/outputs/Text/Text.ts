import { Component, Output, OnInit, Input } from "@angular/core";

@Component({
	selector: 'text-output',
	templateUrl: 'Text.html'
})
export class TextOutput implements OnInit{
	appOutput: any;
	fieldOutput: any;

	@Input() app: any;
	@Input() field: any;

	ngOnInit() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}
}