import {
    Component,
    ComponentFactory,
    NgModule,
    Injectable,
    ModuleWithComponentFactories,
    Compiler
} from '@angular/core';

import { Subject } from 'rxjs/Subject';
import { CommonModule } from '@angular/common'
import { TextComponent } from "../app/core/inputs/text/text.component";
import { NumberComponent } from "../app/core/inputs/number/number.component";
import { BooleanComponent } from "../app/core/inputs/boolean/boolean.component";
import { DropdownComponent } from "../app/core/inputs/dropdown/dropdown.component";
import { DatetimeComponent } from "../app/core/inputs/datetime/datetime.component";
import { PaginatorComponent } from "../app/core/inputs/paginator/paginator.component";



const typeMap = {
    'text': TextComponent,
    'number': NumberComponent,
    'boolean': BooleanComponent,
    'datetime': DatetimeComponent,
    'dropdown': DropdownComponent,
    'paginator': PaginatorComponent
}

function createComponentModule(component: any) {
    @NgModule({
        imports: [CommonModule],
        declarations: [component],
    })
    class RuntimeComponentModule { }

    return RuntimeComponentModule;
}

@Injectable()
export class DynamicLoaderService {
    constructor(protected compiler: Compiler) { }

    private resolveCompHelper$ = new Subject<any>();
    private cache = new Map<string, ComponentFactory<any> | number>();

    public createComponentFactory(type: string): Promise<ComponentFactory<any>> {
        let factory = this.cache.get(type);

        // if factory has been already loading
        if (factory === 1) {
            return new Promise((resolve) => {
                // waiting compilation of factory
                const subscriber = this.resolveCompHelper$.subscribe((data) => {
                    if (type !== data.type) return;
                    subscriber.unsubscribe();
                    resolve(data.factory);
                });
            });
        }
        // factory exists in cache
        if (factory) {
            return new Promise((resolve) => resolve(factory));
        }

        const comp = typeMap[type];
        // factory startes loading
        this.cache.set(type, 1);
        return new Promise((resolve) => {
            this.compiler.compileModuleAndAllComponentsAsync(createComponentModule(comp))
                .then((moduleWithFactories: ModuleWithComponentFactories<any>) => {
                    factory = moduleWithFactories.componentFactories.find(x => x.componentType === comp);
                    console.log(factory);
                    this.cache.set(type, factory);
                    this.resolveCompHelper$.next({ type, factory });

                    resolve(factory);
                });
        });
    }
}