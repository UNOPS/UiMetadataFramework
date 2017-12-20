import Vue from 'vue'
import VueRouter from 'vue-router';
import queryString from 'query-string';

import * as umf from "core-framework";
import { HomeComponent } from 'components/home';
import { AboutComponent } from 'components/about';
import { FormComponent } from 'components/form';
import { NavbarComponent } from 'components/navbar';
import { RouteParameterBuilder } from '../RouteParameterBuilder';

Vue.use(VueRouter);

export class AppRouter implements umf.IAppRouter {
	private readonly rpb: RouteParameterBuilder;
	public router: VueRouter;

	constructor(app: umf.UmfApp) {
		var rpb = this.rpb = new RouteParameterBuilder("_", app);

		this.router = new VueRouter({
			routes: [
				{ path: '/', name: 'home', component: HomeComponent },
				{ path: '/about', name: 'about', component: AboutComponent },
				{
					path: '/form/:_id', name: 'form',
					component: FormComponent,
					caseSensitive: true,
					props: (route) => ({ metadata: route.meta.metadata, form: route.meta.form, app: route.meta.app })
				},
				{ path: '*', redirect: '/' } //404
			],
			scrollBehavior(to, from, savedPosition) {
				return new Promise((resolve, reject) => {
					setTimeout(() => {
						resolve({ x: 0, y: 0 })
					}, 500)
				})
			}
		});

		this.router.beforeEach(function (to, from, next) {
			if (to.name === 'form') {
				rpb.currentForm = to.params._id;
				Object.assign(to.params, to.query);
				var formInstance = app.getFormInstance(to.params._id, true);
				formInstance.initializeInputFields(to.params).then(() => {
					to.meta['metadata'] = formInstance.metadata;
					to.meta['form'] = formInstance;
					to.meta['app'] = app;

					next();
				});
			}
			else {
				next();
			}
		});

		new Vue({
			el: '#app-main',
			components: { 'navbar': NavbarComponent },
			router: this.router,
			data: {
				_app: app
			}
		});
	}

	go(form: string, values) {
		this.router.push({
			path: `/form/${form}`,
			query: this.rpb.buildFormRouteParameters(form, values)
		});
	};

	makeUrl(form: string, values): string {
		return `/form/${form}?${queryString.stringify(this.rpb.buildFormRouteParameters(form, values))}`;
	};
}