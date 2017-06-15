import * as umf from "../../src/core/index";
import Menu from "../svelte-components/menu";
import Form from "../svelte-components/form";

console.log("Vanilla JS client for UiMetadataFramework ");

var server = new umf.UmfServer("http://localhost:62790/api/form/metadata");
var app = new umf.UmfApp(server);

app.load().then(response => {
    var menu = new Menu({
        target: document.getElementById("main"),
        data: {
            name: "Vanilla JS",
            forms: app.forms,
            getUrl: function(form:umf.FormMetadata) {
                return `#/f/${form.id}`;
            }
        }
    });
});