import * as umf from "core-framework";
import { UmfServer, UmfApp } from "core-framework";
import * as handlers from "core-handlers";
import controlRegister from "./ControlRegister";
import { SystemRouter } from './SystemRouter';


export class Register {
	private readonly server: any;
	public readonly app: any;

	constructor(router: any) {
		this.server = new UmfServer("http://localhost:62790/api/form/metadata", "http://localhost:62790/api/form/run");
		this.app = new UmfApp(this.server, controlRegister);

		this.app.load().then(response => {
			var router = new SystemRouter(router, this.app);
			this.app.useRouter(router);

			this.app.registerResponseHandler(new handlers.MessageResponseHandler());
			this.app.registerResponseHandler(new handlers.ReloadResponseHandler((form, inputFieldValues) => {
				return this.app.load().then(t => {
					return this.app.makeUrl(form, inputFieldValues);
				});
			}));

			this.app.registerResponseHandler(new handlers.RedirectResponseHandler((form, inputFieldValues) => {
				this.app.go(form, inputFieldValues);
			}));
		});
	}
}