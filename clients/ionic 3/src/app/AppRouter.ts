
import * as umf from '../../src/core/framework/index';
import { RouteParameterBuilder } from './RouteParameterBuilder';
import { HomePage } from '../pages/home/home';
import { AboutPage } from '../pages/about/about';
import { FormComponent } from '../pages/ui/form';
import { Nav } from 'ionic-angular';
import { ViewChild } from '@angular/core';

export class AppRouter implements umf.IAppRouter {
  static appRoutes: any;
	private readonly rpb: RouteParameterBuilder;
	@ViewChild(Nav) nav:Nav;


	constructor(app: umf.UmfApp) {
		let rpb = this.rpb = new RouteParameterBuilder('_', app);

		// Deeplinks.routeWithNavController(this.nav, {
		// 	'': HomePage,
		// 	'/#/about': AboutPage,
		// 	'/#/form/:id': FormComponent
		//   });

		// Deeplinks.routeWithNavController = [
		// 	{ path: '', component: HomePage },
		// 	{ path: '/about', component: AboutPage },
		// 	{ path: '*', redirectTo: '/' },
		// 	{
		// 		path: '/form/:id',
		// 		component: FormComponent,
		// 		data: (route) => ({ metadata: route.meta.metadata, form: route.meta.form, app: route.meta.app }),
		// 		resolve: function (data, parameters, cb) {
		// 			var formInstance = app.getFormInstance(parameters._id, true);
		// 			formInstance.initializeInputFields(parameters).then(() => {
		// 				cb(false, {
		// 					metadata: formInstance.metadata,
		// 					form: formInstance,
		// 					app: app
		// 				});
		// 			});
		// 		}
		// 	}
		// ];
	}

	go(form: string, values) {
		this.nav.push(`/form/${form}`,
			{ queryParams: this.rpb.buildFormRouteParameters(form, values) });
	}

	makeUrl(form: string, values): string {
		return `/form/${form}?${this.encodeQueryData(this.rpb.buildFormRouteParameters(form, values))}`;
	}

	encodeQueryData(data) {
		let ret = [];
		for (let d in data)
			ret.push(encodeURIComponent(d) + '=' + encodeURIComponent(data[d]));
		return ret.join('&');
	}
}