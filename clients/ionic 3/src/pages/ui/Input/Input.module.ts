import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { InputForm } from '../Input/Input';
import { TextInputModule } from '../inputs/Text.module';
import { TextInput } from '../inputs/Text';


@NgModule({
  declarations: [
    InputForm
  ],
  imports: [
    IonicPageModule.forChild(InputForm),
    TextInputModule
  ],
  exports: [
    InputForm
  ]
})
export class InputFormModule { }
