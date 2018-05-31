import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TextOutput } from './Text';


@NgModule({
  declarations: [
    TextOutput
  ],
  imports: [
    IonicPageModule.forChild(TextOutput),
  ],
  exports: [
    TextOutput
  ]
})
export class TextOutputModule { }
