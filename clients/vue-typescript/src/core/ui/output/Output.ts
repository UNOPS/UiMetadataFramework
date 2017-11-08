import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    template: require('./output.html')
})
export class Output extends Vue {
    showLabel: boolean = true;
    tabindex: number = 1;
    visibleInputFields: any[];
    submitButtonLabel: string;

    field: any;
    app: any;
    form: any;
    parent: any;
    initialized: any;
    self: any;
    alwaysHideLabel: boolean;
    class: string;
    label: string;
    output: any;

    created() {
        this.initialized = this.$attrs["initialized"];

        if (!this.initialized) {
            this.self = this;
            this.initialized = true;
            this.field = this.$attrs["field"];

            this.app = this.$attrs["app"];
            this.parent = this.$attrs["parent"];
            this.form = this.$attrs["form"];
            this.output = this.app.controlRegister.getOutput(this.field).constructor || {};

            var outputDisplayConfig = this.output.constants || {};
            this.alwaysHideLabel = outputDisplayConfig.alwaysHideLabel;

            //new output.constructor({ target: this.$parent, field: this.field, app: this.app, form: this.form, parent: this.parent });

            // Set correct css class based on the field type.
            if (outputDisplayConfig.block) {
                this.class = "block";
            }
            else {
                this.class = "inline";
            }

            this.class = "inline";
        }
    }
}