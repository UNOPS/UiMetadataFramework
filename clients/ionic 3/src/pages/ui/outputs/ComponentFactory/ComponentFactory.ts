import { Component, Input, ViewContainerRef, ViewChild, ReflectiveInjector, ComponentFactoryResolver, OnInit } from '@angular/core';


@Component({
    selector: 'component-factory',
    templateUrl: 'ComponentFactory.html'
})

export class ComponentFactory implements OnInit {
    @Input() field: any = null;
    @Input() app: any = null;
    @Input() form: any = null;
    @Input() parent: any = null;
    @Input() showLabel: boolean = true;
    @ViewChild('output', { read: ViewContainerRef }) entry: ViewContainerRef;
    entryRef: any;


    constructor(private resolver: ComponentFactoryResolver) { }

    ngOnInit() {
        let outputField = this.app.controlRegister.getOutput(this.field);

        this.entry.clear();
        var component = outputField.constructor;
        const factory = this.resolver.resolveComponentFactory(component);

        this.entryRef = this.entry.createComponent(factory);
        this.entryRef.instance.app = this.app;  
        this.entryRef.instance.field = this.field; 
        this.entryRef.instance.form = this.form; 
        this.entryRef.instance.parent = this.parent; 
    }
}

