import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { InputForm } from '../Input/Input';
import { TextInputModule } from '../inputs/Text/Text.module';
import { BooleanInputModule } from '../inputs/Boolean/Boolean.module';
import { DateInputModule } from '../inputs/Date/Date.module';
import { DropdownInputModule } from '../inputs/Dropdown/Dropdown.module';
import { DynamicFormModule } from '../inputs/DynamicForm/DynamicForm.module';
import { MultiSelectInputModule } from '../inputs/MultiSelect/MultiSelect.module';
import { PasswordInputModule } from '../inputs/Password/Password.module';
import { TextareaInputModule } from '../inputs/Textarea/Textarea.module';
import { NumberInputModule } from '../inputs/Number/Number.module';


@NgModule({
  declarations: [
    InputForm
  ],
  imports: [
    IonicPageModule.forChild(InputForm),
    TextInputModule,
    BooleanInputModule,
    DateInputModule,
    DropdownInputModule,
    // DynamicFormModule,
    MultiSelectInputModule,
    NumberInputModule,
    PasswordInputModule,
    TextareaInputModule
  ],
  exports: [
    InputForm
  ]
})
export class InputFormModule { }
