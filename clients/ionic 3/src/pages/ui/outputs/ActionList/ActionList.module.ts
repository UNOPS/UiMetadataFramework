import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ActionList } from './ActionList';
import { FormComponentModule } from '../../form/form.module';
import { ModalModule } from '../Model/Modal.module';


@NgModule({
  declarations: [
    ActionList
  ],
  imports: [
    IonicPageModule.forChild(ActionList),
    ModalModule
  ],
  exports: [
    ActionList
  ]
})
export class ActionListModule { }
