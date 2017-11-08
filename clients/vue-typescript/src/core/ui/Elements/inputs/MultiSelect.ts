import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Multiselect from 'vue-multiselect'
import './Multiselect.scss'
import * as axiosLib from "axios";
var axios = axiosLib.default;

function mapToTypeaheadItems(items) {
	return items.map(t => {
		return {
			label: t.label,
			value: t.value.toString()
		};
	});
}

function setFieldValue(field, value) {
	if (field.maxItemCount == 1) {
		field.value = {
			value: value[0] != null ? value[0].value : null
		};
	}
	else {
		field.value = {
			items: value.map(t => t.value)
		};
	}
}

function setInputValue(a, field) {
	if (field.maxItemCount == 1) {
		let v = (field.value || {}).value || null;
		if (v != null) {
			a.setValueByChoice(v.toString());
		}
	}
	else {
		let v = ((field.value || {}).items || []).map(t => t.toString());
		a.setValueByChoice(v);
	}
}

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
		this.id = this.$attrs["id"];
		this.form = this.$attrs["form"];
		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];
		this.tabindex = parseInt(this.$attrs["tabindex"]);
		this.source = this.field.metadata.customProperties.source;
		this.options = [];
	}

	asyncFind(value) {
		if (typeof (this.source) === "string") {

			var addedItems = {};
			var query = "";
			var timer = null;

			this.isLoading = true;
			var self = this;

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
		this.options = []
	}

	limitText(count) {
		return `and ${count} other options`
	};
}