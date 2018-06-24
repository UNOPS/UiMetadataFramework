import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { OutputForm } from './Output';
import { TableOutputModule } from '../outputs/Table/TableOutput.module';


@NgModule({
  declarations: [
    OutputForm
  ],
  imports: [
    IonicPageModule.forChild(OutputForm),
    TableOutputModule
  ],
  exports: [
    OutputForm
  ],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA ]
})
export class OutputFormModule { }
