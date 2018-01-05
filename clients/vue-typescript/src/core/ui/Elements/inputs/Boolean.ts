import Vue from 'vue';
import { Component } from 'vue-property-decorator';

import './Checkbox.scss';

@Component({
	template: require('./boolean.html')
})
export class BooleanInput extends Vue {
	form: any;
	app: any;
	field: any;
	tabindex: number;
	id: any;

	created() {
		this.id = this.$attrs['id'];
		this.form = this.$attrs['form'];
		this.app = this.$attrs['app'];
		this.field = this.$attrs['field'];
		this.tabindex = parseInt(this.$attrs['tabindex']);
	}
}