import { Component, ElementRef, ViewChild, Input, OnInit } from '@angular/core';
import { NavController } from 'ionic-angular';

@Component({
	selector: 'boolean-input',
	templateUrl: 'Boolean.html'
})
export class BooleanInput implements OnInit {
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
	}
	
	ngOnInit(): void {
		this.inputId = this.id;
		this.inputForm = this.form;
		this.inputApp = this.app;
		this.inputField = this.field;
		this.inputTabindex = this.tabindex;
	}
}