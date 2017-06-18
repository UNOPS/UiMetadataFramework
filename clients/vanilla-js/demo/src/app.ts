import * as umf from "../../src/core/index";
import * as abstractStateRouter from '../../node_modules/abstract-state-router/index';
import * as svelteStateRenderer from '../../node_modules/svelte-state-renderer/index';

import Menu from "../svelte-components/menu";
import Form from "../svelte-components/form";
import Home from "../svelte-components/home";

var server = new umf.UmfServer(
    "http://localhost:62790/api/form/metadata",
    "http://localhost:62790/api/form/run");

var app = new umf.UmfApp(server);

app.load().then(response => {
    const stateRenderer = (<any>svelteStateRenderer).default({});
    const stateRouter = abstractStateRouter.StateProvider(stateRenderer, document.getElementById("main"));

    stateRouter.addState({
        name: "home",
        data: {},
        route: "/home",
        template: Home
    });

    stateRouter.addState({
        name: "form",
        data: {},
        route: "/form/:id",
        template: Form,
        resolve: function(data, parameters, cb) {
            let metadata = app.getForm(parameters.id);
            let formInstance = new umf.FormInstance(metadata);
            
            app.postForm(formInstance);

            cb(false, {
                metadata: metadata,
                form: formInstance,
                app: app
            });
        }
    });

    stateRouter.evaluateCurrentRoute('home');
    
    var menu = new Menu({
        target: document.getElementById("menu"),
        data: {
            name: "Vanilla JS",
            forms: app.forms,
            asr: stateRouter
        }
    });
});