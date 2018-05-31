import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { Alert } from './Alert';


@NgModule({
  declarations: [
    Alert
  ],
  imports: [
    IonicPageModule.forChild(Alert),
  ],
  exports: [
    Alert
  ]
})
export class AlertModule { }
