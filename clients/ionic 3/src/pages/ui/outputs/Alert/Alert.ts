
import { Component, Input } from '@angular/core';

@Component({
	selector: 'alert-output',
	templateUrl: 'Alert.html'
})
export class Alert {
	fieldOutput: any;

	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;
	@Input() showLabel: any;

	created() {
		this.fieldOutput = this.field;
	}
}