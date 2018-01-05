import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import moment from 'moment';

@Component({
	template: require('./FormLink.html')
})
export class FormLink extends Vue {
	app: any;
	field: any;

	created() {
		this.app = this.$attrs['app'];
		this.field = this.$attrs['field'];
	}
}