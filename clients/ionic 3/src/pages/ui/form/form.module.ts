import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { FormComponent } from '.';
import { InputForm } from '../Input/Input';
import { InputFormModule } from '../Input/input.module';


@NgModule({
  declarations: [
    FormComponent
  ],
  imports: [
    IonicPageModule.forChild(FormComponent),
    InputFormModule
  ],
  entryComponents: [
    FormComponent
  ],
  exports: [
    FormComponent
  ]
})
export class FormComponentModule { }
