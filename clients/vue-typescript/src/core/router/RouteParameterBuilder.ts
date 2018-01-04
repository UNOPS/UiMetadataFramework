import * as umf from "core-framework";

export class RouteParameterBuilder {
	readonly parameterName: string;
	currentForm: string;
	getFormInstance: (formId: string, throwError: boolean) => umf.FormInstance;
	defaultParameters: any = {};

	constructor(parameterName: string, app: umf.UmfApp) {
		this.getFormInstance = (formId: string, throwError: boolean) => app.getFormInstance(formId, null);
		this.parameterName = parameterName;
		this.defaultParameters[parameterName] = "";
	}

	buildFormRouteParameters(form, values) {
		var formInstance = this.getFormInstance(form, true);
		var base = formInstance.getSerializedInputValuesFromObject(values);

		if (form === this.currentForm) {
			var d = RouteParameterBuilder.parseQueryStringParameters(location.hash)[this.parameterName] || 0;
			var dAsNumber = parseInt(d, 10);
			base[this.parameterName] = isNaN(dAsNumber) ? 0 : dAsNumber + 1;
		}

		return base;//Object.assign(base, { _id: form });
	}

	static parseQueryStringParameters(url): any {
		var queryStartsAt = url.indexOf("?");

		var result = {};

		// If there is a query string.
		if (queryStartsAt !== -1 && url.length > queryStartsAt) {
			url.substr(queryStartsAt + 1).split("&").filter(t => {
				var value = t.split("=");
				result[value[0]] = value[1];
			});
		}

		return result;
	}
}