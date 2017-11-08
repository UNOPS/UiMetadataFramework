import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { OutputFieldValue } from "core-framework";
import { TableOutput } from "./Table";

@Component({
	template: require('./Paginator.html'),
	components: {
		'TableOutput': TableOutput
	}
})
export class Paginator extends Vue {
	totalCount: 0;

	app: any;
	field: any;
	parent: any;
	form: any;

	tableField: any;

	constructor() {
		super();

		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];
		this.parent = this.$attrs["parent"];
		this.form = this.$attrs["form"];

		this.tableField = new OutputFieldValue();
		this.tableField.data = this.field.data.results;
		this.tableField.metadata = this.field.metadata;
	}

	goToPage(page) {
		var parent = this.$props.get("parent");
		var form = parent.$props.get("form");
		var field = this.$props.get("field");
		var app = this.$props.get("app");

		form.setInputFields(page.params);
		parent.submit(app, form, null, false);
	}

	pages = (field, form) => {
		var paginatorInput = form.inputs.find(t => t.metadata.id == field.metadata.customProperties.customizations.paginator);

		var pageCount = Math.ceil(field.data.totalCount / paginatorInput.value.pageSize);

		var params = {};
		for (let i of form.inputs) {
			params[i.metadata.id] = i.value;
		}

		var pages = [];
		for (let p = 1; p <= pageCount; ++p) {
			let pageParams = Object.assign({}, params);
			pageParams[paginatorInput.metadata.id] = Object.assign({}, pageParams[paginatorInput.metadata.id]);
			pageParams[paginatorInput.metadata.id].pageIndex = p;

			pages.push({
				number: p,
				params: pageParams,
				cssClass: paginatorInput.value.pageIndex == p ? "current" : ""
			});
		}

		return pages;
	}
}