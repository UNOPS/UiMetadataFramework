import { Component, ElementRef, ViewChild, Input } from '@angular/core';
import { NavController } from 'ionic-angular';

@Component({
	selector: 'date-input',
	templateUrl: 'Date.html'
})
export class DateInput {
	inputId: any;
	inputField: any;
	inputForm: any;
	inputTabindex: any;
	inputApp: any;
	@Input() form: any;
	@Input() app: any;
	@Input() field: any;
	@Input() tabindex: number;
	@Input() id: any;

	constructor() {
		this.inputId = this.id;
		this.inputForm = this.form;
		this.inputApp = this.app;
		this.inputField = this.field;
		this.inputTabindex = this.tabindex.valueOf();
	}
}