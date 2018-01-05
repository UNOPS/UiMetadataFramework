import Vue from 'vue';
import { Component } from 'vue-property-decorator';

import './Tabstrip.scss';

@Component({
	template: require('./Tabstrip.html')
})
export class Tabstrip extends Vue {
	app: any;
	field: any;

	created() {
		this.app = this.$attrs['app'];
		this.field = this.$attrs['field'];
	}

	getCssClass(tab, tabstrip) {
		return tab.form === tabstrip.currentTab ? 'active' : '';
	}
}