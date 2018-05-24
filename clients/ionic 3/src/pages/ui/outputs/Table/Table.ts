import { Component, Output } from "@angular/core";

@Component({
	selector: 'table-output',
	templateUrl: 'Table.html'
})
export class TableOutput {
	@Output() field: any = null;
	@Output() app: any = null;
	@Output() form: any = null;
	@Output() parent: any = null;

	fieldOutput: any = null;
	appOutput: any = null;
	formOutput: any = null;
	parentOutput: any = null;

	map: any = null;

	created() {
		this.appOutput = this.appOutput || this.app;
		this.fieldOutput = this.fieldOutput || this.field;
		let data = this.fieldOutput.data;

		this.formOutput = this.formOutput || this.form;
		this.parentOutput = this.parentOutput || this.parent;

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

	getField = function (row, column) {
		let data = row[this.map[column.id.toLowerCase()]];
		return {
			data: data,
			metadata: column
		};
	};

	get columnsOrdered() {
		let columns = this.fieldOutput.metadata.customProperties.columns.slice();
		columns.sort((a, b) => { return a.orderIndex - b.orderIndex; });
		return columns;
	}
}