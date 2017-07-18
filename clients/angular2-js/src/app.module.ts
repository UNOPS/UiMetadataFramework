import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';


import { AppRoutingModule } from './app-routing.module';

import { FormService } from './services/form.service';
import { RestService } from './services/rest.service';

import {
    AppComponent,
    DynamicFormComponent,
    FormListComponent,
    FormViewerComponent
} from './components';
import { MetadataService } from "./services/metadata.service";

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        DynamicFormComponent,
        FormListComponent,
        FormViewerComponent
    ],
    imports: [
        AppRoutingModule,
        BrowserModule,
        HttpModule,
        ReactiveFormsModule
    ],
    providers: [
        FormService,
        RestService,
        MetadataService
    ]
})
export class AppModule {}