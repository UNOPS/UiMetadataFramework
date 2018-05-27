import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { DropdownInput } from './Dropdown';


@NgModule({
  declarations: [
    DropdownInput
  ],
  imports: [
    IonicPageModule.forChild(DropdownInput)
  ],
  entryComponents: [
    DropdownInput
  ],
  exports: [
    DropdownInput
  ]
})
export class DropdownInputModule { }
