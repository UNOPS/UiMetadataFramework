import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { FormComponent } from "core-form";

@Component({
	template: require('./InlineForm.html')
})
export class InlineForm extends Vue {
	app: any;
	field: any;

	constructor() {
		super();

		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];

		this.oncreate();
	}

	oncreate() {
		var formInstance = this.app.getFormInstance(this.field.data.form, true);

		formInstance.initializeInputFields(this.field.data.inputFieldValues).then(() => {
			var f = new FormComponent();
			f.metadata = formInstance.metadata;
			f.form = formInstance;
			f.app = this.app;
			f.useUrl = false;

			f.init();

			this.$props.set({ current: f });
		});
	}
}