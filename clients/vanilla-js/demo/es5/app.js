'use strict';

class $ {
    static get(url) {
        // Return a new promise.
        return new Promise((resolve, reject) => {
            // Do the usual XHR stuff
            var req = new XMLHttpRequest();
            req.open('GET', url);
            req.onload = () => {
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
            req.onerror = () => {
                reject(Error("Network Error"));
            };
            // Make the request
            req.send();
        });
    }
}

class UmfApp {
    getMetadata(formId) {
        return $.get(`/form/metadata/${formId}`).then((response) => {
            console.log(response);
            return response;
        });
    }
    getAllMetadata() {
        return null;
    }
}

console.log("we're in!!");
var app = new UmfApp();
console.log(app);
