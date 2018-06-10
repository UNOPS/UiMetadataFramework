import { Component, Input, ViewContainerRef, ViewChild, ReflectiveInjector, ComponentFactoryResolver, OnInit } from '@angular/core';


@Component({
    selector: 'factory-form',
    templateUrl: 'FormFactory.html'
})

export class FormFactory implements OnInit {
    @Input() metadata: any = null;
    @Input() app: any = null;
    @Input() form: any = null;
    @Input() useUrl: any = null;
    @Input() field: any = null;
    @Input() parent: boolean = true;

    metadataOutput: any = null;
    appOutput: any = null;
    formOutput: any = null;
    useUrlOutput: any = null;
    fieldOutput: any = null;
    parentOutput: boolean = true;

    constructor() { }

    ngOnInit() {
        this.appOutput = this.app;
        this.formOutput = this.form;
        this.metadataOutput = this.metadata;
        this.useUrlOutput = this.useUrl;
        this.parentOutput = this.parent;

    }
}

