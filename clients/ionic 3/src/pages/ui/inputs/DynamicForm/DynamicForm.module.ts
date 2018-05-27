import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DynamicForm } from './DynamicForm';
import { InputFormModule } from '../../Input/Input.module';


@NgModule({
  declarations: [
    DynamicForm
  ],
  imports: [
    IonicPageModule.forChild(DynamicForm),
    InputFormModule
  ],
  entryComponents: [
    DynamicForm
  ],
  exports: [
    DynamicForm
  ]
})
export class DynamicFormModule { }
