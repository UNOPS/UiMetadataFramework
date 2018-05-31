import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { InlineForm } from './InlineForm';


@NgModule({
  declarations: [
    InlineForm
  ],
  imports: [
    IonicPageModule.forChild(InlineForm),
  ],
  exports: [
    InlineForm
  ]
})
export class InlineFormModule { }
