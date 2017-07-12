import * as umf from "../../src/core/index";
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
                asr: stateRouter
            });
        }
    });

    // stateRouter.addState({
    //     name: "form-redirect",
    //     data: {},
    //     route: "/form-redirect/:_id",
    //     template: "",
    //     resolve: function(data, parameters, cb) {
    //         cb.redirect("form", parameters);
    //     }
    // });

    stateRouter.addState({
        name: "form",
        data: {},
        route: "/form/:_id",
        template: Form,
        querystringParameters: ["_x"],
        defaultParameters: { _x: 123 },
        activate: function(context) {
            //console.log(context);
            context.domApi.init();
            debugger;
            //context.domApi.asrReset(context.content);
        },
        resolve: function (data, parameters, cb) {
            console.log("opening form " + parameters._id);
            //debugger;
            var formInstance = app.getFormInstance(parameters._id);

            formInstance.initializeInputFields(parameters).then(() => {
                cb(false, {
                    metadata: formInstance.metadata,
                    form: formInstance,
                    app: app,
                    x: parameters._x
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
        var x = new Date().getMilliseconds();
        var data = Object.assign({ _x: x }, values, { _id: form });

        stateRouter.go("form", data);
    };

    app.makeUrl = (form: string, values): string => {
        var x = new Date().getMilliseconds();
        var data = Object.assign({ _x: x }, values, { _id: form });

        return stateRouter.makePath('form', data);
    };
});

