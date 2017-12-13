import * as umf from "core-framework";

import Vue from 'vue';
import { Component } from 'vue-property-decorator';

import './input.scss'

@Component({
    template: require('./input.html')
})
export class Input extends Vue {
    field: any;
    app: any;
    form: any;
    tabindex: number = 1;
    input: any = {};
    id: any;

    created() {
        this.id = "i" + this.$attrs["id"];
        this.field = this.$attrs["field"];
        this.tabindex = Number.parseInt(this.$attrs["tabindex"]);
        this.app = this.$attrs["app"];
        this.form = this.$attrs["form"];
        this.input = this.app.controlRegister.getInput(this.field.metadata.type).component;
    }
}