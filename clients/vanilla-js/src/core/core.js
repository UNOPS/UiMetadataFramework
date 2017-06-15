"use strict";
exports.__esModule = true;
var _1 = require("./$");
var UmfServer = (function () {
    /**
     * Creates a new instance of UmfApp.
     */
    function UmfServer(getMetadataUrl) {
        this.getMetadataUrl = getMetadataUrl;
    }
    UmfServer.prototype.getMetadata = function (formId) {
        return _1.$.get(this.getMetadataUrl + "/" + formId).then(function (response) {
            return JSON.parse(response);
        })["catch"](function (e) {
            console.warn("Did not find form \"" + formId + "\".");
            return null;
        });
    };
    UmfServer.prototype.getAllMetadata = function () {
        return _1.$.get(this.getMetadataUrl).then(function (response) {
            return JSON.parse(response);
        });
    };
    return UmfServer;
}());
exports.UmfServer = UmfServer;
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
exports.UmfApp = UmfApp;
