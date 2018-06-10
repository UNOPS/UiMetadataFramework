import { Component, ElementRef, ViewChild, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { IonicPage, NavParams, NavController, Events } from 'ionic-angular';
import * as umf from '../../../core/framework/index';
import { UmfApp } from '../../../core/framework/index';


function bindEventHandlersToCustomEvents(formComponent, eventHandlers) {
    let formInstance = formComponent.form;
    let app = formComponent.app;

    // Bind all 'form event handlers'. 
    for (let eventHandler of eventHandlers) {
        // Don't bind default event handlers, because they are already auto-bound inside FormInstance.
        if (eventHandler.runAt.indexOf('form:') === 0) {
            continue;
        }
        formComponent.events.subscribe(eventHandler.runAt, e => {
            // Augment event args with form which is firing the event. This is needed,
            // so that event handler can know from which particular form this event is coming.
            e.form = formComponent;

            formInstance.handleEvent(eventHandler.runAt, eventHandler, e);
        });
    }
}

@IonicPage({
    name: 'form',
    segment: 'form/:id'
})

@Component({
    selector: 'component-form',
    templateUrl: 'form.html'
})

export class FormComponent implements OnInit {

    tabindex: number = 1;
    urlData: null;
    initialized: boolean = false;
    responseMetadata: {};
    useUrl: true;
    params: any;
    visibleInputFields: any;
    outputFieldValues: any;
    disabled: boolean = false;
    self: any;
    submitButtonLabel: any;
    app: any;
    form: any;
    metadata: any;
    nav: any;
    events: any;
    @Input() initializedForm: boolean;

    @Input() appOutput: any = null;
    @Input() formOutput: any = null;
    @Input() useUrlOutput: any = null;
    @Input() fieldOutput: any = null;
    @Input() parentOutput: boolean = true;


    constructor(public paramsCtrl: NavParams,
        public navCtrl: NavController,
        public eventCtrl: Events) {
        this.params = paramsCtrl;
        this.nav = navCtrl;
        this.events = eventCtrl;
    }

    ngOnInit() {
        this.app = this.params.data.app;
        this.form = this.params.data.form;
        this.metadata = this.params.data.metadata;
        this.self = this;
        this.initialized = this.initializedForm;
        this.init();
    }

    async initialiseInputs(field, app) {
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

    init() {
        if (!this.initialized) {
            var form = this.form;
            this.initialized = true;
            this.visibleInputFields = form.inputs.filter(t => t.metadata.hidden == false),
                this.submitButtonLabel = form.metadata.customProperties != null && form.metadata.customProperties.submitButtonLabel
                    ? form.metadata.customProperties.submitButtonLabel
                    : "Submit";

            this.tabindex += 1;

            var app = this.app;

            this.events.publish("form:loaded", { app: app });

            // Auto-submit form if necessary.
            if (form.metadata.postOnLoad) {
                this.submit(app, form, null);
            }
        }
    };
    enableForm() {
        var formInstance = this.form;

        // Hide all inputs, to re-render them. This is needed due to the way that
        // Svelte *seems* to work - it doesn't re-render nested components, unless they are recreated.
        this.visibleInputFields = [];
        this.visibleInputFields = formInstance.inputs.filter(t => t.metadata.hidden == false),
            this.disabled = false;
    };
    renderResponse(response) {
        var formInstance = this.form;

        // Force Svelte to re-render outputs.
        this.outputFieldValues = null;
        this.outputFieldValues = formInstance.outputs;
        this.responseMetadata = response.metadata;
    };
    async submit(app, formInstance, event, redirect = null) {
        if (event != null) {
            event.preventDefault();
        }

        var skipValidation =
            !formInstance.metadata.postOnLoadValidation &&
            formInstance.metadata.postOnLoad &&
            // if initialization of the form, i.e. - first post.
            redirect == null;

        let data = await formInstance.prepareForm(!skipValidation);
        // If not all required inputs are filled.
        if (data == null) {
            return;
        }

        // Disable double-posts.
        this.disabled = true;

        // If postOnLoad == true, then the input field values should appear in the url.
        // Reason is that postOnLoad == true is used by "report" pages, which need
        // their filters to be saved in the url. This does not apply to forms
        // with postOnLoad == false, because those forms are usually for creating new data
        // and hence should not be tracked in browser's history based on parameters.
        if (formInstance.metadata.postOnLoad && redirect) {
            let urlParams = await formInstance.getSerializedInputValues();

            // Update url in the browser.
            app.go(formInstance.metadata.id, urlParams);

            return;
        }

        await formInstance.fire("form:posting", { response: null, app: app });

        try {
            let response = await app.server.postForm(formInstance.metadata.id, data);
            await formInstance.fire("form:responseReceived", { response: response, app: app });

            formInstance.setOutputFieldValues(response);

            // Null response is treated as a server-side error.
            if (response == null) {
                throw new Error(`Received null response.`);
            }

            await app.runFunctions(response.metadata.functionsToRun);

            if (response.metadata.handler == "" || response.metadata.handler == null) {
                this.renderResponse(response);
            }
            else {
                app.handleResponse(response, formInstance);
            }

            await formInstance.fire("form:responseHandled", { response: response, app: app });

            this.enableForm();

            // Signal event to child controls.
            this.events.publish("form:responseHandled", {
                form: self,
                invokedByUser: event != null
            });
        }
        catch (e) {
            this.enableForm();
        }
    }
};
