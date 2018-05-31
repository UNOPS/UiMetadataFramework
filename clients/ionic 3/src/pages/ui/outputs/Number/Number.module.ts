import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { NumberOutput } from './Number';


@NgModule({
  declarations: [
    NumberOutput
  ],
  imports: [
    IonicPageModule.forChild(NumberOutput),
  ],
  exports: [
    NumberOutput
  ]
})
export class NumberOutputModule { }
