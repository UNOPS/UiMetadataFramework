import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import moment from 'moment';

@Component({
	template: require('./Datetime.html')
})
export class DateTimeOutput extends Vue {
	app: any;
	field: any;

	created() {
		this.app = this.$attrs['app'];
		this.field = this.$attrs['field'];
	}

	format = function (datetime: Date) {
		return datetime != null ? moment(datetime).format('D MMM YYYY') : '';
	};
}