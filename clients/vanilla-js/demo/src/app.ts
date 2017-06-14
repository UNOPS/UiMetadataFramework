import * as umf from "../../src/core/index";

console.log("Vanilla JS client for UiMetadataFramework ");

var server = new umf.UmfServer("http://localhost:62790/api/form/metadata");
var app = new umf.UmfApp(server);

app.load().then(response => {
    console.log(app.getForm("UiMetadataFramework.Web.Forms.DoMagic"));
});