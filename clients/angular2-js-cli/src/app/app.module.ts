import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { FormsListComponent } from './forms-list/forms-list-component';
import { MetadataService } from '../services/metadata.service'
import { RestService } from '../services/rest.service'
import { HttpModule } from '@angular/http';
import { AppRoutingModule } from '../app-routing.module';
import { FormViewComponent } from './form-view/form-view.component';
import { FormResolver } from "../form.resolver";
import { MenuComponent } from "./menu/menu.component";
import { DynamicModule } from "./core/dynnamic.module";



@NgModule({
  declarations: [
    AppComponent,
    FormsListComponent,
    FormViewComponent,
    MenuComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    AppRoutingModule, DynamicModule ],
  providers: [
    RestService,
    MetadataService,
    FormResolver
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
