import { Component, Output, OnInit, Input } from "@angular/core";

@Component({
	selector: 'tabstrip-output',
	templateUrl: 'Tabstrip.html'
})
export class Tabstrip implements OnInit{
	@Input() app: any;
	@Input() field: any;

	appOutput: any;
	fieldOutput: any;

	ngOnInit() {
		this.appOutput = this.app;
		this.fieldOutput = this.field;
	}

	getCssClass(tab, tabstrip) {
		return tab.form === tabstrip.currentTab ? 'active' : '';
	}
}