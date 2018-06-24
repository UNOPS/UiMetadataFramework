import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { IonicPageModule, IonicModule } from 'ionic-angular';
import { InputForm } from './Input';
import { TextInputModule } from '../inputs/Text/Text.module';
import { BooleanInputModule } from '../inputs/Boolean/Boolean.module';
import { DateInputModule } from '../inputs/Date/Date.module';
import { DropdownInputModule } from '../inputs/Dropdown/Dropdown.module';
import { MultiSelectInputModule } from '../inputs/MultiSelect/MultiSelect.module';
import { NumberInputModule } from '../inputs/Number/Number.module';
import { PasswordInputModule } from '../inputs/Password/Password.module';


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
    PasswordInputModule

  ],
  exports: [
    InputForm
  ]
})
export class InputFormModule { }
