import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { BooleanComponent } from "../core/inputs/boolean/boolean.component";
import { TextComponent } from "../core/inputs/text/text.component";
import { NumberComponent } from "../core/inputs/number/number.component";
import { DatetimeComponent } from "../core/inputs/datetime/datetime.component";
import { PaginatorComponent } from "../core/inputs/paginator/paginator.component";
import { DropdownComponent } from "../core/inputs/dropdown/dropdown.component";
import { DynamicFieldDirective } from "./inputs/dynamic-field/dynamic-field.directive";
import { DynamicFormComponent } from "./containers/dynamic-form.component";




@NgModule({
    imports: [CommonModule, FormsModule, ReactiveFormsModule],
    declarations: [DynamicFieldDirective,
        DynamicFormComponent,
        BooleanComponent,
        TextComponent,
        DatetimeComponent,
        DropdownComponent,
        NumberComponent,
        PaginatorComponent
    ],
    exports: [DynamicFormComponent],
    entryComponents: [
        BooleanComponent,
        TextComponent,
        DatetimeComponent,
        DropdownComponent,
        NumberComponent,
        PaginatorComponent
    ]
})
export class DynamicModule {
    // static forRoot() {
    //     return {
    //         ngModule: DynamicModule,

    //     };
    // }
}