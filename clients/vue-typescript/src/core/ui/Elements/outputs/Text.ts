import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
	template: require('./Text.html')
})
export class TextOutput extends Vue {
	app: any;
	field: any;

	created() {
		this.app = this.$attrs["app"];
		this.field = this.$attrs["field"];
	}
}