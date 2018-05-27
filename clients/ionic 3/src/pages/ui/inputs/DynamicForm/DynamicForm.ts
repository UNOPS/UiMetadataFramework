import { Component, ElementRef, ViewChild, Input, OnInit } from '@angular/core';

@Component({
	selector: 'dynamic-form',
	templateUrl: 'DynamicForm.html'
})
export class DynamicForm implements OnInit{
	private inputid: number = 0;
	inputs: any[] = [];

	inputField: any;
	inputForm: any;
	inputTabindex: any;
	inputApp: any;
	@Input() form: any;
	@Input() app: any;
	@Input() field: any;
	@Input() tabindex: number;

	ngOnInit(): void {
		this.inputApp = this.app;
		this.inputField = this.field;

		if (this.field.value == null) {
			return;
		}

		this.inputForm = this.form;
		this.inputTabindex = this.tabindex.valueOf();

		let self = this;
		this.initialiseInputs(this.field, this.app).then(() => {
			self.inputs = this.field.inputs;
		});
	}

	get inputId() {
		return this.inputid += 1;
	}

	get id() {
		return `dfi${this.inputId}`;
	}

	initialiseInputs = async function (field, app) {
		field.inputs = app.controlRegister.createInputControllers(field.value.inputs);

		let promises = [];
		for (let input of field.inputs) {
			let i = field.value.inputs.find(t => t.inputId === input.metadata.inputId);
			if (i != null) {
				let p = input.init(i.value);
				promises.push(p);
			}
		}

		await Promise.all(promises);
	};
}