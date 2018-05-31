import * as moment from 'angular2-moment';
import { Component, Input, OnInit } from '@angular/core';

@Component({
	selector: 'formLink-output',
	templateUrl: 'FormLink.html'
})
export class FormLink implements OnInit{
	appOutput: any;
	fieldOutput: any;
	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;
	@Input() showLabel: any;

	ngOnInit() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}
}