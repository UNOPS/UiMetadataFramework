import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TextValue } from './TextValue';


@NgModule({
  declarations: [
    TextValue
  ],
  imports: [
    IonicPageModule.forChild(TextValue),
  ],
  exports: [
    TextValue
  ]
})
export class TextValueModule { }
