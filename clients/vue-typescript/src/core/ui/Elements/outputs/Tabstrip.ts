import Vue from 'vue';
import { Component } from 'vue-property-decorator';

import "./Tabstrip.scss"

@Component({
	template: require('./Tabstrip.html')
})
export class Tabstrip extends Vue {
	getCssClass(tab, tabstrip) {
		return tab.form == tabstrip.currentTab ? "active" : "";
	}
}