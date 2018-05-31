import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { FormInstance } from './FormInstance';


@NgModule({
  declarations: [
    FormInstance
  ],
  imports: [
    IonicPageModule.forChild(FormInstance),
  ],
  exports: [
    FormInstance
  ]
})
export class FormInstanceModule { }
