import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TableOutput } from './Table';
import { OutputFormModule } from '../../Output/Output.module';


@NgModule({
  declarations: [
    TableOutput
  ],
  imports: [
    IonicPageModule.forChild(TableOutput)
  ],
  exports: [
    TableOutput
  ]
})
export class TableOutputModule { }
