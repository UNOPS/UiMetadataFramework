import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { Tabstrip } from './Tabstrip';


@NgModule({
  declarations: [
    Tabstrip
  ],
  imports: [
    IonicPageModule.forChild(Tabstrip),
  ],
  exports: [
    Tabstrip
  ]
})
export class TabstripModule { }
