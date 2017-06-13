(function () {
'use strict';

var $ = (function () {
    function $() {
    }
    $.get = function (url) {
        // Return a new promise.
        return new Promise(function (resolve, reject) {
            // Do the usual XHR stuff
            var req = new XMLHttpRequest();
            req.open('GET', url);
            req.onload = function () {
                // This is called even on 404 etc
                // so check the status
                if (req.status === 200) {
                    // Resolve the promise with the response text
                    resolve(req.response);
                }
                else {
                    // Otherwise reject with the status text
                    // which will hopefully be a meaningful error
                    reject(Error(req.statusText));
                }
            };
            // Handle network errors
            req.onerror = function () {
                reject(Error("Network Error"));
            };
            // Make the request
            req.send();
        });
    };
    return $;
}());

var UmfApp = (function () {
    function UmfApp() {
    }
    UmfApp.prototype.getMetadata = function (formId) {
        return $.get("http://localhost:62790/api/form/metadata/" + formId).then(function (response) {
            console.log(response);
            return response;
        });
    };
    UmfApp.prototype.getAllMetadata = function () {
        return $.get("http://localhost:62790/api/form/metadata/").then(function (response) {
            console.log(response);
            return response;
        });
    };
    return UmfApp;
}());



var umf = Object.freeze({
	UmfApp: UmfApp
});

window.umf = umf;

}());
//# sourceMappingURL=umf-vanilla.js.map