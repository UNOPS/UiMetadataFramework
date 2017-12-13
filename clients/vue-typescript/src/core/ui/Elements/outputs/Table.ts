import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { Output } from "../../output";

import "./Table.scss"

@Component({
	template: require('./Table.html'),
	components: {
		'FormOutput': Output
	}
})
export class TableOutput extends Vue {
	field: any;
	app: any;
	form: any;
	parent: any;
	data: any;
	map: any;

	created() {
		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];
		this.form = this.$attrs["form"];
		this.parent = this.$attrs["parent"];

		this.data = this.field.data;

		// Create map, with key being the lowercase version of the property name
		// and value being the actual property name. 
		var map = {};
		if (this.data.length > 0) {
			let firstRow = this.data[0];

			for (let property in firstRow) {
				if (firstRow.hasOwnProperty(property)) {
					map[property.toLowerCase()] = property;
				}
			}
		}

		this.map = map;
	}

	getField = function (row, column) {
		var data = row[this.map[column.id.toLowerCase()]];
		return {
			data: data,
			metadata: column
		};
	}

	columnsOrdered = function () {
		// return [this.field.metadata.customProperties.columns].sort((a, b) => { return a.orderIndex - b.orderIndex; });
		return this.field.metadata.customProperties.Columns;
	}
}