import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TextareaInput } from './Textarea';


@NgModule({
  declarations: [
    TextareaInput
  ],
  imports: [
    IonicPageModule.forChild(TextareaInput)
  ],
  entryComponents: [
    TextareaInput
  ],
  exports: [
    TextareaInput
  ]
})
export class TextareaInputModule { }
