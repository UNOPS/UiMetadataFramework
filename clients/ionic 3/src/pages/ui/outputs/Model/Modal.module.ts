import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { Modal } from './Modal';


@NgModule({
  declarations: [
    Modal
  ],
  imports: [
    IonicPageModule.forChild(Modal),
  ],
  exports: [
    Modal
  ]
})
export class ModalModule { }
