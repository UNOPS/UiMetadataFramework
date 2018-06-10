import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { OutputForm } from './Output';
import { ActionListModule } from '../outputs/ActionList/ActionList.module';
import { AlertModule } from '../outputs/Alert/Alert.module';
import { DateTimeOutputModule } from '../outputs/Datetime/Datetime.module';
import { DynamicFormModule } from '../inputs/DynamicForm/DynamicForm.module';
import { FormInstanceModule } from '../outputs/FormInstance/FormInstance.module';
import { FormLinkModule } from '../outputs/FormLink/FormLink.module';
import { InlineFormModule } from '../outputs/InlineForm/InlineForm.module';
import { ModalModule } from '../outputs/Model/Modal.module';
import { NumberInputModule } from '../inputs/Number/Number.module';
import { PaginatorModule } from '../outputs/Paginator/Paginator.module';
import { TableOutputModule } from '../outputs/Table/TableOutput.module';
import { TabstripModule } from '../outputs/Tabstrip/Tabstrip.module';
import { TextOutputModule } from '../outputs/Text/TextOutput.module';
import { TextValueModule } from '../outputs/TextValue/TextValue.module';


@NgModule({
  declarations: [
    OutputForm
  ],
  imports: [
    IonicPageModule.forChild(OutputForm),
    ActionListModule,
    AlertModule,
    DateTimeOutputModule,
    // DynamicFormModule,
    // FormInstanceModule,
    FormLinkModule,
    // InlineFormModule,
    // ModalModule,
    NumberInputModule,
    PaginatorModule,
    TabstripModule,
    TextOutputModule,
    TextValueModule
  ],
  exports: [
    OutputForm
  ]
})
export class OutputFormModule { }
