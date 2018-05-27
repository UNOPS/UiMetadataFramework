import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { MultiSelectInput } from './MultiSelect';


@NgModule({
  declarations: [
    MultiSelectInput
  ],
  imports: [
    IonicPageModule.forChild(MultiSelectInput)
  ],
  entryComponents: [
    MultiSelectInput
  ],
  exports: [
    MultiSelectInput
  ]
})
export class MultiSelectInputModule { }
