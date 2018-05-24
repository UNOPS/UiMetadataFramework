import { Component, Output } from "@angular/core";

@Component({
	selector: 'tabstrip-output',
	templateUrl: 'Tabstrip.html'
})
export class Tabstrip {
	@Output() app: any;
	@Output() field: any;

	appOutput: any;
	fieldOutput: any;

	created() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}

	getCssClass(tab, tabstrip) {
		return tab.form === tabstrip.currentTab ? 'active' : '';
	}
}