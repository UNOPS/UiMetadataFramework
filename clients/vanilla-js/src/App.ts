import { UmfServer, UmfApp } from "core-framework";
import * as handlers from "core-handlers";

import controlRegister from "./ControlRegister";
import { AppRouter } from "./AppRouter";

var server = new UmfServer(
    "http://localhost:62790/api/form/metadata",
    "http://localhost:62790/api/form/run");

var app = new UmfApp(server, controlRegister);

app.load().then(response => {
    var router = new AppRouter(document.getElementById("main"), app);
    app.useRouter(router);

    app.registerResponseHandler(new handlers.MessageResponseHandler());
    app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
        app.go(form, inputFieldValues);
    }));
});