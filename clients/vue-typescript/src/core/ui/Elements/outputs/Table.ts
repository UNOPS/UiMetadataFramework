import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { Output } from '../../output';

import './Table.scss';

@Component({
	template: require('./Table.html'),
	components: {
		'FormOutput': Output
	}
})
export class TableOutput extends Vue {
	field: any = null;
	app: any = null;
	form: any = null;
	parent: any = null;
	map: any = null;

	created() {
		this.app = this.app || this.$attrs['app'];
		this.field = this.field || this.$attrs['field'];
		let data = this.field.data;

		this.form = this.form || this.$attrs['form'];
		this.parent = this.parent || this.$attrs['parent'];

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
		let columns = this.field.metadata.customProperties.Columns.slice();
		columns.sort((a, b) => { return a.orderIndex - b.orderIndex; });
		return columns;
	}
}