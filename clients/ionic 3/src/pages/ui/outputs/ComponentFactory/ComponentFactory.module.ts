import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ComponentFactory } from './ComponentFactory';
import { OutputFormModule } from '../../Output';


@NgModule({
  declarations: [
    ComponentFactory
  ],
  imports: [
    IonicPageModule.forChild(ComponentFactory),
    OutputFormModule
  ],
  exports: [
    ComponentFactory
  ]
})
export class ComponentFactoryModule { }
