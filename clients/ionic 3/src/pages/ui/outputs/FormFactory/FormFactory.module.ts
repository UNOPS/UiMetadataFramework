import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { FormFactory } from './FormFactory';
import { FormComponentModule } from '../../form/form.module';
import { InputFormModule } from '../../Input/input.module';
import { OutputFormModule } from '../../Output/Output.module';


@NgModule({
  declarations: [
    FormFactory
  ],
  imports: [
    IonicPageModule.forChild(FormFactory),
    FormComponentModule
  ],
  exports: [
    FormFactory
  ]
})
export class FormFactoryModule { }
