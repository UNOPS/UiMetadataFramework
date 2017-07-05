import * as umf from "../../src/core/index";
import * as handlers from "./handlers/index";
import * as abstractStateRouter from "../../node_modules/abstract-state-router/index";
import * as svelteStateRenderer from "../../node_modules/svelte-state-renderer/index";

import Menu from "../svelte-components/menu";
import Form from "../svelte-components/form";
import Home from "../svelte-components/home";

var server = new umf.UmfServer(
    "http://localhost:62790/api/form/metadata",
    "http://localhost:62790/api/form/run");

var app = new umf.UmfApp(server);

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

    stateRouter.addState({
        name: "form",
        data: {},
        route: "/form/:_id",
        template: Form,
        resolve: function (data, parameters, cb) {
            let metadata = app.getForm(parameters._id);
            
            if (metadata == null) {
                console.error(`Form ${parameters._id} not found.`);
                return;
            }

            let formInstance = new umf.FormInstance(metadata, parameters);

            cb(false, {
                metadata: metadata,
                form: formInstance,
                app: app
            });
        }
    });

    stateRouter.evaluateCurrentRoute("home");

    app.registerResponseHandler(new handlers.MessageResponseHandler());
    app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
        var data = Object.assign({}, inputFieldValues, { _id: form });
        stateRouter.go("form", data);
    }));

    app.go = (form:string, values) => {
        var data = Object.assign({}, values, { _id: form });
        
        stateRouter.go("form", data);
    };
});

