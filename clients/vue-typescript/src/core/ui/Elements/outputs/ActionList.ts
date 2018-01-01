import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { FormComponent } from "core-form";
import { Modal } from "./Modal";
import bus from '../../../event-bus';

import "./ActionList.scss"

@Component({
	template: require('./ActionList.html'),
	components: { 'modal': Modal, 'FormComponent': FormComponent }
})
export class ActionList extends Vue {
	open: boolean = false;
	current: any = null;
	modalId = 0;

	modals = [];

	field: any;
	app: any;
	form: any;
	parent: any;
	data: any;
	modalComponent: any = {};
	invokedByUser: boolean;

	created() {
		this.field = this.$attrs["field"];
		this.app = this.$attrs["app"];
		this.form = this.$attrs["form"];
		this.parent = this.$attrs["parent"];

		bus.$on("form:responseHandled", e => {
			this.invokedByUser = e.invokedByUser;
		});
	}

	run = function (action, app) {
		this.open = true;
		this.modalId += 1;

		var formInstance = app.getFormInstance(action.form, true);

		// TODO: find a way to initialize from action.inputFieldValues directly.
		var serializedInputValues = formInstance.getSerializedInputValuesFromObject(action.inputFieldValues);

		formInstance.initializeInputFields(serializedInputValues).then(() => {

			// this.$nextTick(() => {});

			this.modalComponent = {
				metadata: formInstance.metadata,
				form: formInstance,
				app: app,
				useUrl: false
			}

			var self = this;

			var f = null;

			f = new FormComponent({
				data: this.modalComponent
			});

			f.init();

			if (self.invokedByUser && formInstance.metadata.closeOnPostIfModal) {
				self.close(true);
			}

			this.current = this;
		});

		this.modals.push(this);
	}

	close(reloadParentForm) {
		// Ensure the modal div is hidden.
		this.open = false;

		// Destroy underlying form instance.
		// var modalForm = this.current;
		// modalForm.$destroy();

		if (reloadParentForm) {
			// Refresh parent form.
			var app = this.app;
			var form = this.form;

			this.parent.submit(app, form, null, false);
		}

		this.modals.slice(this.modals.findIndex(a => a == this));
	}
}