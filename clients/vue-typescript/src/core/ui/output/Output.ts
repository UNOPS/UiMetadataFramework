import Vue from 'vue';
import { Component } from 'vue-property-decorator';

import './Output.scss';

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
    self: any;
    alwaysHideLabel: boolean;
    classObj: string;
    label: string;
    output: any;

    created() {
        this.self = this;
        this.field = this.$attrs["field"];
        this.app = this.$attrs["app"];
        this.parent = this.$attrs["parent"];
        this.form = this.$attrs["form"];
        this.output = this.app.controlRegister.getOutput(this.field).constructor || {};

        var outputDisplayConfig = this.output.constants || {};
        this.alwaysHideLabel = outputDisplayConfig.alwaysHideLabel;

        // Set correct css class based on the field type.
        if (outputDisplayConfig.block) {
            this.classObj = "block";
        }
        else {
            this.classObj = "inline";
        }
    }
}