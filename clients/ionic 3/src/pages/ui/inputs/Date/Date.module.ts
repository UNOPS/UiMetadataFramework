import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DateInput } from './Date';


@NgModule({
  declarations: [
    DateInput
  ],
  imports: [
    IonicPageModule.forChild(DateInput)
  ],
  entryComponents: [
    DateInput
  ],
  exports: [
    DateInput
  ]
})
export class DateInputModule { }
