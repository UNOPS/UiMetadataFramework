import Vue from 'vue';
import VueRouter from 'vue-router';
import { HomeComponent } from 'components/home';
import { AboutComponent } from 'components/about';
import { NavbarComponent } from 'components/navbar';
import { FormComponent } from 'components/form';

import { Register } from './Register'
var registry = new Register(this);
Vue.use(VueRouter);

const AppRouter =  new VueRouter({
	routes: [
		{ path: '/', name: 'home', component: HomeComponent },
		{ path: '/about', name: 'about', component: AboutComponent },
		{
			path: '/form/:_id', name: 'form',
			component: FormComponent,
			caseSensitive: true,
			props: (route) => ({ metadata: route.meta.metadata, form: route.meta.form, app: route.meta.app }),
			beforeEnter(to, from, next) {
				var formInstance = registry.app.getFormInstance(to.params._id, true);

				formInstance.initializeInputFields(to.params).then(function (cb) {
					to.meta['metadata'] = formInstance.metadata;
					to.meta['form'] = formInstance;
					to.meta['app'] = registry.app;
				});

				next();
			}
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

export default AppRouter;