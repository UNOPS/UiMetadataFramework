import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TextInput } from './Text';


@NgModule({
  declarations: [
    TextInput
  ],
  imports: [
    IonicPageModule.forChild(TextInput)
  ],
  entryComponents: [
    TextInput
  ],
  exports: [
    TextInput
  ]
})
export class TextInputModule { }
