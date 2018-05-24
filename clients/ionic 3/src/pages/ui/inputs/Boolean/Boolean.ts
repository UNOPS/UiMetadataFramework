import { Component, ElementRef, ViewChild, Input } from '@angular/core';
import { NavController } from 'ionic-angular';

@Component({
	selector: 'boolean-input',
	templateUrl: 'Boolean.html'
})
export class BooleanInput {
	@Input() form: any;
	@Input() app: any;
	@Input() field: any;
	@Input() tabindex: number;
	@Input() id: any;

	inputId: any;
	inputField: any;
	inputForm: any;
	inputTabindex: any;
	inputApp: any;

	constructor() {
		this.inputId = this.id;
		this.inputForm = this.form;
		this.inputApp = this.app;
		this.inputField = this.field;
		this.inputTabindex = this.tabindex;
	}
}