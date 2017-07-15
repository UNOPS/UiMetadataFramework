import * as umf from "../../src/index";
import * as abstractStateRouter from "../../node_modules/abstract-state-router/index";
import * as svelteStateRenderer from "../../node_modules/svelte-state-renderer/index";

import Menu from "../svelte-components/Menu";
import Form from "../svelte-components/Form";
import Home from "../svelte-components/Home";

export class AppRouter implements umf.IAppRouter {
	private readonly stateRenderer: any;
	private readonly stateRouter: any;
	private readonly element: HTMLElement;
	private readonly rpb: RouteParameterBuilder;

	constructor(element: HTMLElement, app: umf.UmfApp) {
		this.element = element;
		this.stateRenderer = (<any>svelteStateRenderer).default({});
		this.stateRouter = (<any>abstractStateRouter).default(this.stateRenderer, this.element);
		var rpb = this.rpb = new RouteParameterBuilder("_", app);

		this.stateRouter.addState({
			name: "home",
			data: {},
			route: "/home",
			template: Home
		});

		var self = this;
		this.stateRouter.addState({
			name: "menu",
			route: "/menu",
			template: Menu,
			resolve: function (data, parameters, cb) {
				cb(false, {
					forms: app.forms,
					makeUrl: (form:string) => self.makeUrl(form, null)
				});
			}
		});

		this.stateRouter.addState({
			name: "form",
			data: {},
			route: "/form/:_id",
			template: Form,

			// Force route reload when value of _d parameter changes. This is
			// needed because by default the router will not reload route even if
			// any of the parameters change, unless they are specified in "querystringParameters".
			// This means that if we are trying to reload same form, but with different parameters,
			// nothing will happen, unless _d changes too.
			querystringParameters: [rpb.parameterName],
			defaultParameters: rpb.defaultParameters,

			activate: function (context) {
				context.domApi.init();

				rpb.currentForm = context.parameters._id;
				context.on("destroy", () => rpb.currentForm = null);
			},
			resolve: function (data, parameters, cb) {
				var formInstance = app.getFormInstance(parameters._id, true);

				formInstance.initializeInputFields(parameters).then(() => {
					cb(false, {
						metadata: formInstance.metadata,
						form: formInstance,
						app: app
					});
				});
			}
		});

		this.stateRouter.evaluateCurrentRoute("home");
	}

	go(form: string, values) {
		this.stateRouter.go("form", this.rpb.buildFormRouteParameters(form, values));
	};

	makeUrl(form: string, values): string {
		return this.stateRouter.makePath('form', this.rpb.buildFormRouteParameters(form, values));
	};
}

class RouteParameterBuilder {
	readonly parameterName: string;
	currentForm: string;
	getFormInstance:(formId: string, throwError: boolean) => umf.FormInstance;
	defaultParameters: any = {};

	constructor(parameterName: string, app: umf.UmfApp) {
		this.getFormInstance = (formId: string, throwError: boolean) => app.getFormInstance(formId, null);
		this.parameterName = parameterName;
		this.defaultParameters[parameterName] = "";
	}

	buildFormRouteParameters(form, values) {
		var formInstance = this.getFormInstance(form, true);
		var base = formInstance.getSerializedInputValuesFromObject(values);

		if (form === this.currentForm) {
			var d = RouteParameterBuilder.parseQueryStringParameters(location.hash)[this.parameterName] || 0;
			var dAsNumber = parseInt(d, 10);
			base[this.parameterName] = isNaN(dAsNumber) ? 0 : dAsNumber + 1;
		}

		return Object.assign(base, { _id: form });
	}

	static parseQueryStringParameters(url): any {
		var queryStartsAt = url.indexOf("?");

		var result = {};

		// If there is a query string.
		if (queryStartsAt !== -1 && url.length > queryStartsAt) {
			url.substr(queryStartsAt + 1).split("&").filter(t => {
				var value = t.split("=");
				result[value[0]] = value[1];
			});
		}

		return result;
	}
}
