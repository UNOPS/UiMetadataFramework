import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { IonicPageModule, IonicModule } from 'ionic-angular';
import { FormComponent } from './form';
import { InputFormModule } from '../Input/Input.module';
import { OutputFormModule } from '../Output';


@NgModule({
  declarations: [
    FormComponent
  ],
  imports: [
    IonicPageModule.forChild(FormComponent),
    InputFormModule,
    OutputFormModule
  ],
  entryComponents: [
  ],
  exports: [
    FormComponent
  ],
})
export class FormComponentModule { }
