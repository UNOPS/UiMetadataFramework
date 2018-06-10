import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DynamicOutput } from './DynamicOutput';
import { OutputFormModule } from '../../Output';


@NgModule({
  declarations: [
    DynamicOutput
  ],
  imports: [
    IonicPageModule.forChild(DynamicOutput),
    OutputFormModule
  ],
  exports: [
    DynamicOutput
  ]
})
export class DynamicOutputModule { }
