import * as umf from "../../src/core/index";

console.log("Vanilla JS client for UiMetadataFramework ");

var app = new umf.UmfApp();

app.getMetadata("UiMetadataFramework.Web.Forms.DoMagic").then(response => {
    console.log(response);
});