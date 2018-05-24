import { Component, ElementRef, ViewChild, Input, ViewContainerRef, ComponentFactoryResolver, OnInit } from '@angular/core';
import { IonicPage } from 'ionic-angular';
import { TextInput } from '../inputs/Text';
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
        console.log(this.inputApp);

        this.entry.clear();
        var component = TextInput
        debugger;
        console.log();
        const factory = this.resolver.resolveComponentFactory(component);
        let entryRef = this.entry.createComponent(factory);
        entryRef.instance.app = this.app;  
        entryRef.instance.field = this.field; 
        entryRef.instance.tabindex = this.tabindex; 
        entryRef.instance.form = this.form; 
        entryRef.instance.id = this.id; 
    }

    constructor(private resolver: ComponentFactoryResolver) {

    }
}