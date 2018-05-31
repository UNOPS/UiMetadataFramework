import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { TableOutput } from './Table';
import { OutputFormModule } from '../../Output/Output.module';
import { ComponentFactoryModule } from '../ComponentFactory/ComponentFactory.module';


@NgModule({
  declarations: [
    TableOutput
  ],
  imports: [
    IonicPageModule.forChild(TableOutput),
    ComponentFactoryModule
    
  ],
  exports: [
    TableOutput
  ]
})
export class TableOutputModule { }
