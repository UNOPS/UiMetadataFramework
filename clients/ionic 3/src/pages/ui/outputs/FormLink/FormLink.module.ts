import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { FormLink } from './FormLink';


@NgModule({
  declarations: [
    FormLink
  ],
  imports: [
    IonicPageModule.forChild(FormLink),
  ],
  exports: [
    FormLink
  ]
})
export class FormLinkModule { }
