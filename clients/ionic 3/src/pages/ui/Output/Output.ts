import { Component, ElementRef, ViewChild, Input, ViewContainerRef, ComponentFactoryResolver, OnInit } from '@angular/core';


@Component({
	selector: 'output-form',
	templateUrl: 'output.html'
})
export class OutputForm implements OnInit{
    showLabel: boolean = true;
    tabindex: number = 1;
    visibleInputFields: any[];
    submitButtonLabel: string;

    field: any;
    app: any;
    form: any;
    parent: any;
    alwaysHideLabel: boolean;
    classObj: string;
    @ViewChild('output', { read: ViewContainerRef }) entry: ViewContainerRef;
    constructor(private resolver: ComponentFactoryResolver){
    }
    @Input() outputField: any;
    @Input() outputForm: any;
    @Input() outputParent: any;
    @Input() outputApp: any;
    @Input() outputshowLabel : any;
    entryRef: any;

    ngOnInit() {
        debugger;
        this.field = this.outputField;
        this.app = this.outputApp;
        this.parent = this.outputParent;
        this.form = this.outputForm;

        if (this.outputshowLabel != null) {
            this.showLabel = new Boolean(this.outputshowLabel).valueOf();
        }

        let outputField = this.app.controlRegister.getOutput(this.field);

        this.entry.clear();
        var component = outputField.constructor;
        const factory = this.resolver.resolveComponentFactory(component);

        this.entryRef = this.entry.createComponent(factory);
        this.entryRef.instance.app = this.app;  
        this.entryRef.instance.field = this.field; 
        this.entryRef.instance.form = this.form; 
        this.entryRef.instance.parent = this.parent; 

        let outputDisplayConfig = outputField.constants || {};
        this.alwaysHideLabel = outputDisplayConfig.alwaysHideLabel;

        // Set correct css class based on the field type.
        if (outputDisplayConfig.block) {
            this.classObj = 'block';
        }
        else {
            this.classObj = 'inline';
        }
    }
}