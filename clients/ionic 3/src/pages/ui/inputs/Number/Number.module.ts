import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { NumberInput } from './Number';


@NgModule({
  declarations: [
    NumberInput
  ],
  imports: [
    IonicPageModule.forChild(NumberInput)
  ],
  entryComponents: [
    NumberInput
  ],
  exports: [
    NumberInput
  ]
})
export class NumberInputModule { }
