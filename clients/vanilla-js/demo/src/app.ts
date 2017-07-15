import * as umf from "../../src/index";
import * as handlers from "./handlers/index";

import inputRegister from "./InputRegister";
import { AppRouter } from "./AppRouter";

var server = new umf.UmfServer(
    "http://localhost:62790/api/form/metadata",
    "http://localhost:62790/api/form/run");

var app = new umf.UmfApp(server, inputRegister);

app.load().then(response => {
    app.router = new AppRouter(document.getElementById("main"), app);

    app.registerResponseHandler(new handlers.MessageResponseHandler());
    app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
        app.router.go(form, inputFieldValues);
    }));
});

