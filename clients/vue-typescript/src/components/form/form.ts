import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import { Output } from '../../core/ui/output';
import { Input } from '../../core/ui/input';

import './form.scss'

@Component({
    template: require('./form.html'),
    components: {
        'FormOutput': Output,
        'FormInput': Input
    }
})
export class FormComponent extends Vue {
    initialized: boolean;
    visibleInputFields: any[];
    submitButtonLabel: string;
    tabindex: number = 1;
    disabled: false;
    responseMetadata: any = {};
    urlData: null;
    useUrl: boolean;
    metadata: any;
    form: any;
    app: any;
    outputFieldValues: any = null;
    parent: any;

    created() {
        this.app = this.app || this.$attrs["app"];
        this.form = this.form || this.$attrs["form"];
        this.metadata = this.metadata || this.$attrs["metadata"];
        this.parent = this.parent || this;
        this.initialized = this.initialized || false;
        this.tabindex += parseInt(this.$attrs["tabindex"]) || 1;
        this.useUrl = this.useUrl || new Boolean(this.$attrs["useUrl"]).valueOf();

        if (!this.initialized) {
            this.init();
        }
    }

    init = function () {
        if (!this.initialized) {
            this.initialized = true;
            this.visibleInputFields = this.form.inputs.filter(t => t.metadata.hidden == false);
            this.submitButtonLabel = this.form.metadata.customProperties != null && this.form.metadata.customProperties.submitButtonLabel
                ? this.form.metadata.customProperties.submitButtonLabel
                : "Submit"

            this.form.fire("form:loaded", { app: this.app });

            // Auto-submit form if necessary.
            if (this.form.metadata.postOnLoad) {
                this.submit(this.app, this.form);
            }
        }
    };

    enableForm = function () {
        var formInstance = this.form;

        this.visibleInputFields = formInstance.inputs.filter(t => t.metadata.hidden == false);
        this.disabled = false;
    };

    renderResponse = function (response: any) {
        var formInstance = this.form;

        this.outputFieldValues = formInstance.outputs;
        this.responseMetadata = response.metadata;
    };

    submit = async function (app, formInstance, event, redirect) {
        var self = this;

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
        self.disabled = true;

        // If postOnLoad == true, then the input field values should appear in the url.
        // Reason is that postOnLoad == true is used by "report" pages, which need
        // their filters to be saved in the url. This does not apply to forms
        // with postOnLoad == false, because those forms are usually for creating new data
        // and hence should not be tracked in browser's history based on parameters.
        if (formInstance.metadata.postOnLoad && redirect && this.useUrl) {
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
                self.renderResponse(response);
            }
            else {
                app.handleResponse(response, formInstance);
            }

            await formInstance.fire("form:responseHandled", { response: response, app: app });

            self.enableForm();

            // Signal event to child controls.
            self.fire("form:responseHandled", {
                form: self,
                invokedByUser: event != null
            });
        }
        catch (e) {
            self.enableForm();
        }
    };
}
