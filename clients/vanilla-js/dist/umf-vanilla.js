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
    $.post = function (url, data) {
        return new Promise(function (resolve, reject) {
            var params = typeof data == 'string' ? data : Object.keys(data).map(function (k) { return encodeURIComponent(k) + '=' + encodeURIComponent(data[k]); }).join('&');
            var xhr = window.XMLHttpRequest ? new XMLHttpRequest() : new ActiveXObject("Microsoft.XMLHTTP");
            xhr.open('POST', url);
            xhr.onreadystatechange = function () {
                if (xhr.readyState > 3 && xhr.status == 200) {
                    resolve(xhr.responseText);
                }
                else {
                    reject(Error(xhr.status));
                }
            };
            xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
            xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            xhr.send(params);
        });
    };
    return $;
}());

/**
 * Encapsulates all information needed to render a form.
 */
var FormMetadata = (function () {
    function FormMetadata() {
    }
    return FormMetadata;
}());

/**
 * Represents a reference to a form.
 */
var FormLink = (function () {
    function FormLink() {
    }
    return FormLink;
}());

/**
 * Represents response of a form.
 */
var FormResponse = (function () {
    function FormResponse() {
    }
    return FormResponse;
}());

/**
 * Represents metadata for a single input field. *
 */
var InputFieldMetadata = (function () {
    function InputFieldMetadata() {
    }
    return InputFieldMetadata;
}());

/**
* Represents a source from which a value can be retrieved. This class can be useful for
* binding input field values to a specific data source.
*/
var InputFieldSource = (function () {
    function InputFieldSource() {
    }
    return InputFieldSource;
}());

/**
 * Represents metadata for a single output field.
 */
var OutputFieldMetadata = (function () {
    function OutputFieldMetadata() {
    }
    return OutputFieldMetadata;
}());

var UmfServer = (function () {
    /**
     * Creates a new instance of UmfApp.
     */
    function UmfServer(getMetadataUrl, postFormUrl) {
        this.getMetadataUrl = getMetadataUrl;
        this.postFormUrl = postFormUrl;
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
    UmfServer.prototype.postForm = function (formInstance) {
        return $.post(this.postFormUrl, [{
                requestId: 1,
                form: formInstance.metadata.id,
                inputFieldValues: formInstance.inputFieldValues,
            }]);
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
    UmfApp.prototype.postForm = function (formInstance) {
        return this.server.postForm(formInstance);
    };
    return UmfApp;
}());

var FormInstance = (function () {
    function FormInstance(metadata) {
        this.inputFieldValues = {};
        this.metadata = metadata;
    }
    return FormInstance;
}());



var umf = Object.freeze({
	UmfServer: UmfServer,
	UmfApp: UmfApp,
	FormMetadata: FormMetadata,
	FormLink: FormLink,
	FormResponse: FormResponse,
	InputFieldMetadata: InputFieldMetadata,
	InputFieldSource: InputFieldSource,
	OutputFieldMetadata: OutputFieldMetadata,
	FormInstance: FormInstance
});

window.umf = umf;

}());
//# sourceMappingURL=umf-vanilla.js.map
