declare global {
	interface Window {
		// ReSharper disable once InconsistentNaming
		XMLHttpRequest: any;
	}
}

declare var XMLHttpRequest: any;
declare var ActiveXObject: any;

export class $ {
	static get(url: string) {
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

	static post(url: string, data: any) {
		return new Promise((resolve, reject) => {
			var params = typeof data == 'string' ? data : Object.keys(data).map(
				function (k) { return encodeURIComponent(k) + '=' + encodeURIComponent(data[k]) }
			).join('&');

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
	}
}