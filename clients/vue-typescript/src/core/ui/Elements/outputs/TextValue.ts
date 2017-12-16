import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
	template: require('./TextValue.html')
})
export class TextValue extends Vue {
	app: any;
	field: any;

	created() {
		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];
	}
}