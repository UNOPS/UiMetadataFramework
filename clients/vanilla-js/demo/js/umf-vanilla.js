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

var UmfServer = (function () {
    /**
     * Creates a new instance of UmfApp.
     */
    function UmfServer(getMetadataUrl) {
        this.getMetadataUrl = getMetadataUrl;
    }
    UmfServer.prototype.getMetadata = function (formId) {
        return $.get(this.getMetadataUrl + "/" + formId).then(function (response) {
            return JSON.parse(response);
        }).catch(function (e) {
            console.warn("Did not find form \"" + formId + "\".");
            return null;
        });
    };
    UmfServer.prototype.getAllMetadata = function () {
        return $.get(this.getMetadataUrl).then(function (response) {
            return JSON.parse(response);
        });
    };
    return UmfServer;
}());
var UmfApp = (function () {
    function UmfApp(server) {
        this.formsById = {};
        this.server = server;
    }
    UmfApp.prototype.load = function () {
        var _this = this;
        return this.server.getAllMetadata()
            .then(function (response) {
            _this.forms = response;
            for (var _i = 0, _a = _this.forms; _i < _a.length; _i++) {
                var form = _a[_i];
                _this.formsById[form.id] = form;
            }
        });
    };
    UmfApp.prototype.getForm = function (id) {
        return this.formsById[id];
    };
    return UmfApp;
}());



var umf = Object.freeze({
	UmfServer: UmfServer,
	UmfApp: UmfApp
});

window.umf = umf;

}());
//# sourceMappingURL=umf-vanilla.js.map
