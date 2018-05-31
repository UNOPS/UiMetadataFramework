import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { ComponentFactory } from './ComponentFactory';


@NgModule({
  declarations: [
    ComponentFactory
  ],
  imports: [
    IonicPageModule.forChild(ComponentFactory)
  ],
  exports: [
    ComponentFactory
  ]
})
export class ComponentFactoryModule { }
