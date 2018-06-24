import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { MyApp } from './app.component';
import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { AppRouter } from './AppRouter';
import { AutoCompleteModule } from 'ionic2-auto-complete';
import { Deeplinks } from '@ionic-native/deeplinks';
import { OutputFormModule } from '../pages/ui/Output';
import { InputFormModule } from '../pages/ui/Input/Input.module';
import { TextareaInputModule } from '../pages/ui/inputs/Textarea/Textarea.module';
import { PasswordInputModule } from '../pages/ui/inputs/Password/Password.module';
import { NumberInputModule } from '../pages/ui/inputs/Number/Number.module';
import { MultiSelectInputModule } from '../pages/ui/inputs/MultiSelect/MultiSelect.module';
import { DropdownInputModule } from '../pages/ui/inputs/Dropdown/Dropdown.module';
import { DateInputModule } from '../pages/ui/inputs/Date/Date.module';
import { BooleanInputModule } from '../pages/ui/inputs/Boolean/Boolean.module';
import { TextInputModule } from '../pages/ui/inputs/Text/Text.module';
  



@NgModule({
  declarations: [
    MyApp,
    HomePage,
    ListPage
  ],
  imports: [
    BrowserModule,
    IonicModule.forRoot(MyApp,{
      preloadModules: true
      
    }),
    AutoCompleteModule,
    InputFormModule,
    OutputFormModule
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp,
    HomePage,
    ListPage,
  ],
  providers: [
    StatusBar,
    SplashScreen,
    Deeplinks,
    { provide: ErrorHandler, useClass: IonicErrorHandler }
  ]
})
export class AppModule { }