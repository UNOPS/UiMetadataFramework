import * as moment from 'angular2-moment';
import { Component, Input } from '@angular/core';

@Component({
	selector: 'formLink-output',
	templateUrl: 'FormLink.html'
})
export class FormLink {
	appOutput: any;
	fieldOutput: any;
	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;
	@Input() showLabel: any;

	created() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}
}