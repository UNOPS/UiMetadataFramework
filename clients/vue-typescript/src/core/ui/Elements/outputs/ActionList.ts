import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { FormComponent } from "core-form";
import { Modal } from "./Modal";

@Component({
	template: require('./ActionList.html'),
	components: {
		'modal': Modal,
		'FormComponent': FormComponent
	}
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
	modalComponent: any;

	created() {
		this.field = this.$attrs["field"];
		this.app = this.$attrs["app"];
		this.form = this.$attrs["form"];
		this.parent = this.$attrs["parent"];
	}

	run(action, app) {
		this.open = true;
		this.modalId += 1;
		var formInstance = app.getFormInstance(action.form, true);

		// TODO: find a way to initialize from action.inputFieldValues directly.
		var serializedInputValues = formInstance.getSerializedInputValuesFromObject(action.inputFieldValues);

		formInstance.initializeInputFields(serializedInputValues).then(() => {

			this.modalComponent = new FormComponent({
				data: {
					metadata: formInstance.metadata,
					form: formInstance,
					app: app,
					useUrl: false,
					initialized: true
				}
			});

			this.modalComponent.init();

			this.$nextTick(() => {
				//console.log(new Date());
			});

			var self = this;

			this.modalComponent.$on("form:responseHandled", e => {
				if (e.invokedByUser && formInstance.metadata.closeOnPostIfModal) {
					self.close(true);
				}
			});

			this.current = this.modalComponent;
		});

		this.modals.push(this);
	}

	close(reloadParentForm) {
		// Ensure the modal div is hidden.
		this.open = true;

		// Destroy underlying form instance.
		var modalForm = this.current;
		modalForm.$destroy();

		if (reloadParentForm) {
			// Refresh parent form.
			var app = this.app;
			var form = this.form;

			this.parent.submit(app, form, null, true);
		}

		this.modals.slice(this.modals.findIndex(a => a == this));
	}
}