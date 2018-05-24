import * as umf from '../../src/core/framework/index';

export class RouteParameterBuilder {
	readonly parameterName: string;
	currentForm: string;
	getFormInstance: (formId: string, throwError: boolean) => umf.FormInstance;
	defaultParameters: any = {};

	constructor(parameterName: string, app: umf.UmfApp) {
		this.getFormInstance = (formId: string, throwError: boolean) => app.getFormInstance(formId, null);
		this.parameterName = parameterName;
		this.defaultParameters[parameterName] = '';
	}

	buildFormRouteParameters(form, values) {
		let formInstance = this.getFormInstance(form, true);
		let base = formInstance.getSerializedInputValuesFromObject(values);

		if (form === this.currentForm) {
			let d = RouteParameterBuilder.parseQueryStringParameters(location.hash)[this.parameterName] || 0;
			let dAsNumber = parseInt(d, 10);
			base[this.parameterName] = isNaN(dAsNumber) ? 0 : dAsNumber + 1;
		}

		return base;
	}

	static parseQueryStringParameters(url): any {
		let queryStartsAt = url.indexOf('?');

		let result = {};

		// If there is a query string.
		if (queryStartsAt !== -1 && url.length > queryStartsAt) {
			url.substr(queryStartsAt + 1).split('&').filter(t => {
				let value = t.split('=');
				result[value[0]] = value[1];
			});
		}

		return result;
	}
}