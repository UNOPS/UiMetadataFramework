import * as umf from "../../src/index";
import * as handlers from "./handlers/index";
import * as abstractStateRouter from "../../node_modules/abstract-state-router/index";
import * as svelteStateRenderer from "../../node_modules/svelte-state-renderer/index";

import Menu from "../svelte-components/Menu";
import Form from "../svelte-components/Form";
import Home from "../svelte-components/Home";

import { DateInputController } from "./inputs/DateInputController";
import { NumberInputController } from "./inputs/NumberInputController";
import { DropdownInputController } from "./inputs/DropdownInputController";
import { BooleanInputController } from "./inputs/BooleanInputController";

var inputRegister = new umf.InputControllerRegister();
inputRegister.controllers["datetime"] = DateInputController;
inputRegister.controllers["number"] = NumberInputController;
inputRegister.controllers["dropdown"] = DropdownInputController;
inputRegister.controllers["boolean"] = BooleanInputController;

var server = new umf.UmfServer(
    "http://localhost:62790/api/form/metadata",
    "http://localhost:62790/api/form/run");

var app = new umf.UmfApp(server, inputRegister);
app.load().then(response => {
    const stateRenderer = (<any>svelteStateRenderer).default({});
    const stateRouter = (<any>abstractStateRouter).default(stateRenderer, document.getElementById("main"));
    let rpb = new RouteParameterBuilder("_");

    stateRouter.addState({
        name: "home",
        data: {},
        route: "/home",
        template: Home
    });

    stateRouter.addState({
        name: "menu",
        route: "/menu",
        template: Menu,
        resolve: function (data, parameters, cb) {
            cb(false, {
                forms: app.forms,
                app: app
            });
        }
    });

    stateRouter.addState({
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

    stateRouter.evaluateCurrentRoute("home");

    app.registerResponseHandler(new handlers.MessageResponseHandler());
    app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
        var data = Object.assign({}, inputFieldValues, { _id: form });
        stateRouter.go("form", data);
    }));

    app.go = (form: string, values) => {
        stateRouter.go("form", rpb.buildFormRouteParameters(form, values));
    };

    app.makeUrl = (form: string, values): string => {
        return stateRouter.makePath('form', rpb.buildFormRouteParameters(form, values));
    };
});

class RouteParameterBuilder {
    readonly parameterName: string;
    currentForm: string;
    defaultParameters: any = {};

    constructor(parameterName: string) {
        this.parameterName = parameterName;
        this.defaultParameters[parameterName] = "";
    }

    buildFormRouteParameters(form, values) {
        var formInstance = app.getFormInstance(form, true);
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
