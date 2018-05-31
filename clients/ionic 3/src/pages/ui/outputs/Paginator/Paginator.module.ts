import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { Paginator } from './Paginator';
import { TableOutputModule } from '../Table/TableOutput.module';


@NgModule({
  declarations: [
    Paginator
  ],
  imports: [
    IonicPageModule.forChild(Paginator),
    TableOutputModule
  ],
  exports: [
    Paginator
  ]
})
export class PaginatorModule { }
