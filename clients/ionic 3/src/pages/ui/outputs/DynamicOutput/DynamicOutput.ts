import * as umf from '../../../../core/framework/index';
import { Component, Input, OnInit } from '@angular/core';

@Component({
	selector: 'dynamic-output',
	templateUrl: 'DynamicOutput.html'
})

export class DynamicOutput implements OnInit{
	formOutput: any;
	appOutput: any;
	fieldOutput: any;
	parentOutput: any;
	items: any[] = [];

	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;

	ngOnInit() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
		this.formOutput = this.form;
		this.parentOutput = this.parent;

		let metadata = this.field.metadata;
		let items = [];

		for (let item of this.field.data.items) {
			items.push(umf.FormInstance.getOutputFieldValues(metadata, item));
		}

		this.items = items;
	}
}