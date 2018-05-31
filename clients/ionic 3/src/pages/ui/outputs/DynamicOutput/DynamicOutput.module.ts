import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DynamicOutput } from './DynamicOutput';


@NgModule({
  declarations: [
    DynamicOutput
  ],
  imports: [
    IonicPageModule.forChild(DynamicOutput),
  ],
  exports: [
    DynamicOutput
  ]
})
export class DynamicOutputModule { }
