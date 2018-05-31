import { Component, Output, OnInit, Input, ViewChild, ViewContainerRef, ComponentFactoryResolver, ViewChildren, QueryList } from "@angular/core";

@Component({
	selector: 'table-output',
	templateUrl: 'Table.html'
})
export class TableOutput implements OnInit {
	@Input() field: any = null;
	@Input() app: any = null;
	@Input() form: any = null;
	@Input() parent: any = null;
	@Input() outputshowLabel: any = null;

	fieldOutput: any = null;
	appOutput: any = null;
	formOutput: any = null;
	parentOutput: any = null;
	showLabel: boolean = true;
	alwaysHideLabel: boolean;
	classObj: string;

	entryRef: any;

	map: any = null;
	constructor(private resolver: ComponentFactoryResolver) {
	}

	ngOnInit() {
		debugger;
		this.appOutput = this.app;
		this.fieldOutput = this.field;
		let data = this.fieldOutput.data;

		this.formOutput = this.form;
		this.parentOutput = this.parent;

		if (this.outputshowLabel != null) {
			this.showLabel = new Boolean(this.outputshowLabel).valueOf();
		}

		let outputField = this.app.controlRegister.getOutput(this.field);
		let outputDisplayConfig = outputField.constants || {};
		this.alwaysHideLabel = outputDisplayConfig.alwaysHideLabel;

		// Set correct css class based on the field type.
		if (outputDisplayConfig.block) {
			this.classObj = 'block';
		}
		else {
			this.classObj = 'inline';
		}
		//

		// Create map, with key being the lowercase version of the property name
		// and value being the actual property name. 
		let map = [];
		if (data.length > 0) {
			let firstRow = data[0];

			for (let property in firstRow) {
				if (firstRow.hasOwnProperty(property)) {
					map[property.toLowerCase()] = property;
				}
			}
		}

		this.map = map;
	}

	getField(row, column) {
		let data = row[this.map[column.id.toLowerCase()]];
		return {
			data: data,
			metadata: column
		};
	};

	columnsOrdered() {
		let columns = this.fieldOutput.metadata.customProperties.Columns.slice();
		columns.sort((a, b) => { return a.orderIndex - b.orderIndex; });
		return columns;
	}
}