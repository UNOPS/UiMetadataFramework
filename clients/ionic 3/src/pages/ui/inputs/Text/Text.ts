import { Component, Input, OnInit } from "@angular/core";
import { IonicPage } from "ionic-angular";

@IonicPage()
@Component({
	selector: 'text-input',
	templateUrl: 'Text.html'
})
export class TextInput implements OnInit{

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

	ngOnInit(): void {
		this.inputId = this.id;
		this.inputForm = this.form;
		this.inputApp = this.app;
		this.inputField = this.field;
		this.inputTabindex = this.tabindex.valueOf();
	}

}