import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ActionList } from './ActionList';
import { FormComponentModule } from '../../form/form.module';


@NgModule({
  declarations: [
    ActionList
  ],
  imports: [
    IonicPageModule.forChild(ActionList),
    FormComponentModule
  ],
  exports: [
    ActionList
  ]
})
export class ActionListModule { }
