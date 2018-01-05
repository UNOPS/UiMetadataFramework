import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Multiselect from 'vue-multiselect';
import * as axiosLib from 'axios';
let axios = axiosLib.default;

import './Multiselect.scss';

@Component({
	template: require('./MultiSelect.html'),
	components: { Multiselect }
})
export class MultiSelectInput extends Vue {
	form: any;
	app: any;
	field: any;
	tabindex: number;
	id: any;

	source: any[];
	options: any[] = [];
	isLoading: boolean = false;

	created() {
		this.id = this.$attrs['id'];
		this.form = this.$attrs['form'];
		this.app = this.$attrs['app'];
		this.field = this.$attrs['field'];
		this.tabindex = parseInt(this.$attrs['tabindex']);
		this.source = this.field.metadata.customProperties.Source;
		this.options = [];
	}

	asyncFind(value) {
		if (typeof (this.source) === 'string') {

			let addedItems = {};
			let query = '';
			let timer = null;

			this.isLoading = true;
			let self = this;

			if (timer != null) {
				// Cancel previous timer, thus extending the delay until user has stopped typing.
				clearTimeout(timer);
			}

			// Search when user types something, but introduce a short delay
			// to avoid excessive http requests.
			timer = setTimeout(function () {
				self.app.server.postForm(self.source, { query: value }).then(data => {
					self.options = data.items;
					self.isLoading = false;
				});
			}, 300);
		}
	}

	clearAll() {
		this.options = [];
	}

	limitText(count) {
		return `and ${count} other options`;
	}
}