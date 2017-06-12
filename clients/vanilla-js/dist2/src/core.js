import { $ } from "./$";
var UmfApp = (function () {
    function UmfApp() {
    }
    UmfApp.prototype.getMetadata = function (formId) {
        return $.get("/form/metadata/" + formId).then(function (response) {
            console.log(response);
            return response;
        });
    };
    UmfApp.prototype.getAllMetadata = function () {
        return null;
    };
    return UmfApp;
}());
export { UmfApp };
//# sourceMappingURL=core.js.map