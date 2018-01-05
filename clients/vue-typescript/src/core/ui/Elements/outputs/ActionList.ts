import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import { FormComponent } from 'core-form';
import { Modal } from './Modal';
import EventBus from 'core/event-bus';

import './ActionList.scss';

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

	created() {
		this.field = this.$attrs['field'];
		this.app = this.$attrs['app'];
		this.form = this.$attrs['form'];
		this.parent = this.$attrs['parent'];
	}

	beforeDestroy() {
		EventBus.$off('form:responseHandled');
	}

	run = function (action, app) {
		this.open = true;
		this.modalId += 1;

		let formInstance = app.getFormInstance(action.form, true);

		// TODO: find a way to initialize from action.inputFieldValues directly.
		let serializedInputValues = formInstance.getSerializedInputValuesFromObject(action.inputFieldValues);

		let self = this;

		formInstance.initializeInputFields(serializedInputValues).then(() => {

			this.modalComponent = {
				metadata: formInstance.metadata,
				form: formInstance,
				app: app,
				parent: self.parent,
				useUrl: false
			};

			this.current = this;

			EventBus.$on('form:responseHandled', e => {
				if (e.invokedByUser && formInstance.metadata.closeOnPostIfModal) {
					self.close(true);
				}
			});
		});

		this.modals.push(this);
	};

	close(reloadParentForm) {
		// Ensure the modal div is hidden.
		this.open = false;

		EventBus.$off('form:responseHandled');

		if (reloadParentForm) {
			// Refresh parent form.
			let app = this.app;
			let form = this.form;
			this.parent.submit(app, form, null, true);
		}

		this.modals.slice(this.modals.findIndex(a => a === this));
	}
}