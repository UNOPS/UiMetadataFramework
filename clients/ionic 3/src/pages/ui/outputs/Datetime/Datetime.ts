import * as moment from 'angular2-moment';
import { Component, Input, OnInit } from '@angular/core';


@Component({
	selector: 'datetime-output',
	templateUrl: 'Datetime.html'
})
export class DateTimeOutput implements OnInit{
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

	format = function (datetime: Date) {
		return datetime != null ? datetime : '';
	};
}