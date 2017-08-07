import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlFactoryDirective } from "../control-factory.directive";
import { DynamicLoaderService } from "../../services/dynamic-loader.service";


@NgModule({
    imports: [CommonModule],
    declarations: [ControlFactoryDirective],
    exports: [ControlFactoryDirective]
})
export class DynamicModule {
    static forRoot() {
        return {
            ngModule: DynamicModule,
            providers: [DynamicLoaderService],
        };
    }
}