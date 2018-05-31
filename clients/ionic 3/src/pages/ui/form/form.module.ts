import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { InputForm } from '../Input/Input';
import { InputFormModule } from '../Input/input.module';
import { OutputFormModule } from '../Output/Output.module';
import { FormComponent } from './form';


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
    FormComponent
  ],
  exports: [
    FormComponent
  ]
})
export class FormComponentModule { }
