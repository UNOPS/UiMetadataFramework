import { Component, ElementRef, ViewChild, Input, OnInit } from '@angular/core';
import { NavController } from 'ionic-angular';

@Component({
	selector: 'date-input',
	templateUrl: 'Date.html'
})
export class DateInput implements OnInit {
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
	}

	ngOnInit(): void {
		this.inputId = this.id;
		this.inputForm = this.form;
		this.inputApp = this.app;
		this.inputField = this.field;
		this.inputTabindex = this.tabindex.valueOf();
	}
}