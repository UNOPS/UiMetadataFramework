import { OutputFieldValue } from '../../../../core/framework/index';
import { TableOutput } from '../Table/Table';
import { Output, Component } from '@angular/core';

@Component({
	selector: 'paginator-output',
	templateUrl: 'Paginator.html'
})
export class Paginator {
	totalCount: number = 0;
	@Output() app: any;
	@Output() field: any;
	@Output() parent: any;
	@Output() form: any;

	tableField: any;
	appOutput: any;
	fieldOutput: any;
	parentOutput: any;
	formOutput: any;

	created() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
		this.parentOutput = this.parent;
		this.formOutput = this.form;

		this.tableField = new OutputFieldValue();
		this.tableField.data = this.field.data.results;
		this.tableField.metadata = this.field.metadata;
	}

	goToPage(page) {
		let parent = this.parent;
		let form = parent.form;
		let field = this.field;
		let app = this.app;

		form.setInputFields(page.params);
		parent.submit(app, form, null, false);
	}

	get pages() {
		let paginatorInput = this.form.inputs.find(t => t.metadata.id === this.fieldOutput.metadata.customProperties.customizations.paginator);

		let pageCount = Math.ceil(this.fieldOutput.data.totalCount / paginatorInput.value.pageSize);

		let params = {};
		for (let i of this.form.inputs) {
			params[i.metadata.id] = i.value;
		}

		let pages = [];
		for (let p = 1; p <= pageCount; ++p) {
			let pageParams = Object.assign({}, params);
			pageParams[paginatorInput.metadata.id] = Object.assign({}, pageParams[paginatorInput.metadata.id]);
			pageParams[paginatorInput.metadata.id].pageIndex = p;

			pages.push({
				number: p,
				params: pageParams,
				cssClass: paginatorInput.value.pageIndex === p ? 'current' : ''
			});
		}

		return pages;
	}

}