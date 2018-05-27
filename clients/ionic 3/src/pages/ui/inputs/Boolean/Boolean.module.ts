import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { BooleanInput } from './Boolean';


@NgModule({
  declarations: [
    BooleanInput
  ],
  imports: [
    IonicPageModule.forChild(BooleanInput)
  ],
  entryComponents: [
    BooleanInput
  ],
  exports: [
    BooleanInput
  ]
})
export class BooleanInputModule { }
