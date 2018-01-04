import { UmfServer, UmfApp } from "core-framework";
import * as handlers from "core-handlers";
import controlRegister from "./Core/ControlRegister";
import { AppRouter } from "./Core/Router/AppRouter";

import './sass/main.scss';
import { Vue } from "vue/types/vue";

var coreServerUrl = "http://localhost:62790";

var server = new UmfServer(`${coreServerUrl}/api/form/metadata`, `${coreServerUrl}/api/form/run`);
var app = new UmfApp(server, controlRegister);

app.load().then(response => {
    var router = new AppRouter(app);
    app.useRouter(router);

    app.registerResponseHandler(new handlers.MessageResponseHandler());
    app.registerResponseHandler(new handlers.ReloadResponseHandler((form, inputFieldValues) => {
        return app.load().then(t => {
            return app.makeUrl(form, inputFieldValues);
        });
    }));

    app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
        app.go(form, inputFieldValues);
    }));
});