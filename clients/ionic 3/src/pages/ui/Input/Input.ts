import { Component, ElementRef, ViewChild, Input, ViewContainerRef, ComponentFactoryResolver, OnInit } from '@angular/core';
import { IonicPage } from 'ionic-angular';
import { TextInput } from '../inputs/Text/Text';
@IonicPage()
@Component({
    selector: 'input-form',
    templateUrl: 'Input.html'
})
export class InputForm implements OnInit {

    field: any;
    app: any;
    form: any;
    tabindex: number = 1;
    input: any = {};
    id: any;
    entryRef: any;

    @Input() inputId: any;
    @Input() inputField: any;
    @Input() inputForm: any;
    @Input() inputTabindex: any;
    @Input() inputApp: any;

    @ViewChild('input', { read: ViewContainerRef }) entry: ViewContainerRef;

    ngOnInit() {
        this.id = 'i' + this.inputId;
        this.field = this.inputField;
        this.tabindex = Number.parseInt(this.inputTabindex);
        this.app = this.inputApp;
        this.form = this.inputForm;
        this.entry.clear();
        var component = this.app.controlRegister.getInput(this.field.metadata.type).component;
        debugger;
        const factory = this.resolver.resolveComponentFactory(component);
        this.entryRef = this.entry.createComponent(factory);
        this.entryRef.instance.app = this.app;  
        this.entryRef.instance.field = this.field; 
        this.entryRef.instance.tabindex = this.tabindex; 
        this.entryRef.instance.form = this.form; 
        this.entryRef.instance.id = this.id; 
    }
    constructor(private resolver: ComponentFactoryResolver) {

    }
}