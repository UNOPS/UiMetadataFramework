import { Component, Input } from "@angular/core";

@Component({
	selector: 'inlineform-output',
	templateUrl: 'InlineForm.html'
})
export class InlineForm {
	appOutput: any;
	fieldOutput: any;
	parentOutput: any;

	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;
	data: any = {};

	created() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
		this.parentOutput = this.parent;

		let formInstance = this.app.getFormInstance(this.field.data.form, true);

		formInstance.initializeInputFields(this.field.data.inputFieldValues).then(() => {
			this.data = {
				metadata: formInstance.metadata,
				form: formInstance,
				app: this.app,
				parent: this.parent,
				useUrl: false
			};
		});
	}
}