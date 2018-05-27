import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { PasswordInput } from './Password';


@NgModule({
  declarations: [
    PasswordInput
  ],
  imports: [
    IonicPageModule.forChild(PasswordInput)
  ],
  entryComponents: [
    PasswordInput
  ],
  exports: [
    PasswordInput
  ]
})
export class PasswordInputModule { }
