import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DateTimeOutput } from './Datetime';


@NgModule({
  declarations: [
    DateTimeOutput
  ],
  imports: [
    IonicPageModule.forChild(DateTimeOutput),
  ],
  exports: [
    DateTimeOutput
  ]
})
export class DateTimeOutputModule { }
