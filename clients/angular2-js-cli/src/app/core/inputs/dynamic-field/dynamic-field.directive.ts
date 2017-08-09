import { ComponentFactoryResolver, Directive, Input, OnInit, ViewContainerRef, ComponentRef } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { InputFieldMetadata } from "uimf-core/src";
import { TextComponent } from "../text/text.component";
import { NumberComponent } from "../number/number.component";
import { BooleanComponent } from "../boolean/boolean.component";
import { DatetimeComponent } from "../datetime/datetime.component";
import { DropdownComponent } from "../dropdown/dropdown.component";
import { PaginatorComponent } from "../paginator/paginator.component";

const components = {
    'text': TextComponent,
    'number': NumberComponent,
    'boolean': BooleanComponent,
    'datetime': DatetimeComponent,
    'dropdown': DropdownComponent,
    'paginator': PaginatorComponent
};

@Directive({
    selector: '[dynamicField]'
})
export class DynamicFieldDirective implements OnInit {
    component: ComponentRef<any>;
    @Input()
    config: InputFieldMetadata;

    @Input()
    group: FormGroup;

    constructor(
        private resolver: ComponentFactoryResolver,
        private container: ViewContainerRef
    ) { }

    ngOnInit() {
        if (!components[this.config.type]) {
            const supportedTypes = Object.keys(components).join(', ');
            throw new Error(
                `Trying to use an unsupported type (${this.config.type}).
        Supported types: ${supportedTypes}`
            );
        }
        const component = this.resolver.resolveComponentFactory<any>(components[this.config.type]);
        this.component = this.container.createComponent(component);
        this.component.instance.config = this.config;
        this.component.instance.group = this.group;
    }
}