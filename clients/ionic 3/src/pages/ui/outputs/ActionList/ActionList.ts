import { Modal } from '../Model/Modal';
import { ActionListEventArguments } from './ActionListEventArguments';

import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { EventBusService } from '../../../../core/event-bus';

@Component({
	selector: 'actionlist-output',
	templateUrl: 'ActionList.html'
})
export class ActionList implements OnInit{

	eventBusService: any;
	open: boolean = false;
	current: any = null;
	modalid = 0;
	modals = [];

	@Input() field: any;
	@Input() app: any;
	@Input() form: any;
	@Input() parent: any;
	@Input() showLabel: any;

	data: any;
	modalComponent: any = {};
	private initialized: boolean = false;

	outputField: any;
	outputForm: any;
	outputParent: any;
	outputApp: any;
	outputshowLabel: any;
	constructor(eventBusService : EventBusService){

	}

	get modalId() {
		return this.modalid;
	}
	ngOnInit() {
		this.outputField = this.field;
		this.outputApp = this.app;
		this.outputForm = this.form;
		this.outputParent = this.parent;
		this.modalid += 1;
	}

	beforeDestroy() {
		this.eventBusService.FormResponseHandled.unsubsribe();
	}

	run = async function (action, app) {
		let self = this;
		let formInstance = app.getFormInstance(action.form, true);

		// TODO: find a way to initialize from action.inputFieldValues directly.
		let serializedInputValues = formInstance.getSerializedInputValuesFromObject(action.inputFieldValues);
		await formInstance.initializeInputFields(serializedInputValues);

		let allRequiredInputsHaveData = await formInstance.allRequiredInputsHaveData(false);

		if (action.action === 'run' && allRequiredInputsHaveData) {
			await formInstance.submit(this.app, false);
			this.onActionRun(formInstance.metadata.id);
		}
		else {
			this.open = true;

			this.modalComponent = {
				metadata: formInstance.metadata,
				form: formInstance,
				app: app,
				parent: self.parent,
				useUrl: false
			};

			this.initialized = true;

			this.eventBusService.FormResponseHandled.subscribe( e => {
				if (e.invokedByUser && formInstance.metadata.closeOnPostIfModal) {
					self.close(true);
				}
			});

			this.current = self;
			this.modals.push(self);
		}
	};

	close(reloadParentForm) {
		// Ensure the modal div is hidden.
		this.open = false;

		this.eventBusService.FormResponseHandled.unsubsribe();

		if (reloadParentForm) {
			let formId = this.form.metadata.id;
			this.onActionRun(formId);
		}

		this.modals.slice(this.modals.findIndex(a => a === this));
	}

	async onActionRun(formId) {
		let parentForm = this.parent;
		let app = parentForm.app;
		let formInstance = parentForm.form;

		await parentForm.submit(app, formInstance, null, true);

		let eventArgs = new ActionListEventArguments(app, formId);
		parentForm.fireAndBubbleUp(this.eventBusService.actionListRunEvent, eventArgs);
	}
}