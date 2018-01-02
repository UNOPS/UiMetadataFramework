import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { FormComponent } from "core-form";

import "./InlineForm.scss"

@Component({
	template: require('./InlineForm.html'),
	components: {
		'FormComponent': FormComponent
	}
})
export class InlineForm extends Vue {
	app: any;
	field: any;
	parent: any;
	current: any;
	data: any = {};
	initialized: boolean = false;

	created() {
		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];
		this.parent = this.$attrs["parent"];

		var formInstance = this.app.getFormInstance(this.field.data.form, true);

		formInstance.initializeInputFields(this.field.data.inputFieldValues).then(() => {
			this.data = {
				metadata: formInstance.metadata,
				form: formInstance,
				app: this.app,
				parent: this.parent,
				useUrl: false
			};

			var f = new FormComponent({
				data: this.data
			});

			f.init();

			this.initialized = f.initialized;

			this.current = f;
		});
	}
}