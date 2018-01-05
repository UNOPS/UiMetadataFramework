import { FormMetadata, FormResponse, FormResponseMetadata, ClientFunctionMetadata } from 'uimf-core';
import { UmfServer } from './UmfServer';
import { FormInstance } from './FormInstance';
import { IFormResponseHandler } from './IFormResponseHandler';
import { InputFieldValue } from './InputFieldValue';
import { ControlRegister } from './ControlRegister';
import { IAppRouter } from './IAppRouter';

export class UmfApp implements IAppRouter {
	forms: FormMetadata[];
	private readonly formsById: { [id: string]: FormMetadata } = {};
	public readonly server: UmfServer;
	public readonly formResponseHandlers: { [id: string]: IFormResponseHandler } = {};
	public controlRegister: ControlRegister;
	go: (form: string, values: any) => void;
	makeUrl: (form: string, values: any) => string;

	constructor(server: UmfServer, inputRegister: ControlRegister) {
		this.server = server;
		this.controlRegister = inputRegister;
	}

	useRouter(router: IAppRouter) {
		this.go = (form: string, values: any) => {
			return router.go(form, values);
		};

		this.makeUrl = (form: string, values: any) => {
			return router.makeUrl(form, values);
		};
	}

	registerResponseHandler(handler: IFormResponseHandler) {
		this.formResponseHandlers[handler.name] = handler;
	}

	load() {
		return this.server.getAllMetadata()
			.then(response => {
				this.forms = response;

				for (let form of this.forms) {
					this.formsById[form.id] = form;
				}
			});
	}

	getForm(id: string) {
		return this.formsById[id];
	}

	getFormInstance(formId: string, throwError: boolean = false) {
		let metadata = this.getForm(formId);

		if (metadata == null) {
			if (throwError) {
				let error = Error(`Form ${formId} not found.`);
				console.error(error);
				throw error;
			}

			return null;
		}

		return new FormInstance(metadata, this.controlRegister);
	}

	handleResponse(response: FormResponse, form: FormInstance) {
		let responseMetadata = response.metadata || new FormResponseMetadata();
		let handler = this.formResponseHandlers[responseMetadata.handler];

		if (handler == null) {
			let error = new Error(`Cannot find FormResponseHandler "${responseMetadata.handler}".`);
			console.error(error);
			throw error;
		}

		return handler.handle(response, form);
	}

	runFunctions(functionMetadata: ClientFunctionMetadata[]) {
		if (functionMetadata == null) {
			return Promise.resolve();
		}

		let promises = [];
		for (let f of functionMetadata) {
			let handler = this.controlRegister.functions[f.id];

			if (handler == null) {
				throw new Error(`Could not find function '${f.id}'.`);
			}

			let promise = handler.run(f);
			promises.push(promise);
		}

		return Promise.all(promises);
	}
}