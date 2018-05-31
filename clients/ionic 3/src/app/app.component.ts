import { Component, ViewChild } from '@angular/core';
import { Nav, Platform, NavController, NavParams } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';

import { HomePage } from '../pages/home/home';
import { ListPage } from '../pages/list/list';
import { UmfServer, UmfApp } from '../../src/core/framework/index';
import controlRegister from './ControlRegister';
import { AppRouter } from './AppRouter';
import * as handlers from '../core/handlers/index';

@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  @ViewChild(Nav) nav: Nav;

  rootPage: any = HomePage;

  pages: Array<{ title: string, component: any }>;
  forms: any;
  app:any;
  metadata:any;
  form:any;

  constructor(public platform: Platform,
     public statusBar: StatusBar,
      public splashScreen: SplashScreen) {
    this.initializeApp();
  }

  initializeApp() {
    this.platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      this.statusBar.styleDefault();
      this.splashScreen.hide();
      var server = new UmfServer(
        "/local/api/form/metadata",
        "/local/api/form/run");

      this.app = new UmfApp(server, controlRegister);
      this.app.load().then(response => {
          var router = new AppRouter(this.app);
          this.app.useRouter(router);
          this.forms = this.app.forms;

          this.app.registerResponseHandler(new handlers.FormComponentResponseHandler());
          this.app.registerResponseHandler(new handlers.MessageResponseHandler());
          this.app.registerResponseHandler(new handlers.ReloadResponseHandler((form, inputFieldValues) => {
            return this.app.load().then(t => {
              return this.app.makeUrl(form, inputFieldValues);
            });
          }));
          this.app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
            this.app.go(form, inputFieldValues);
          }));
        });


    });
  }
	initialUrl(form: string){
    var formInstance = this.app.getFormInstance(form, true);
    formInstance.initializeInputFields(null).then(() => {
        this.metadata = formInstance.metadata;
        this.form = formInstance;
        this.nav.push('form', {
          id: form,
          app: this.app,
          metadata: this.metadata,
          form: this.form
        });
      });

  }
}
