import * as umf from "../../src/index";
import * as handlers from "./handlers/index";

import inputRegister from "./InputRegister";
import { AppRouter } from "./AppRouter";

var server = new umf.UmfServer(
    "http://localhost:62790/api/form/metadata",
    "http://localhost:62790/api/form/run");

var app = new umf.UmfApp(server, inputRegister);

app.load().then(response => {
    var router = new AppRouter(document.getElementById("main"), app);
    app.useRouter(router);

    app.registerResponseHandler(new handlers.MessageResponseHandler());
    app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
        app.go(form, inputFieldValues);
    }));
});

