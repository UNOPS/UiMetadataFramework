import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
	template: require('./Number.html')
})
export class NumberOutput extends Vue {
	app: any;
	field: any;

	constructor() {
		super();

		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];
	}
}