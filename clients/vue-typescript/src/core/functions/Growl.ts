import * as umf from "uimf-core";
import { IFunctionRunner } from "core-framework";

export class Growl implements IFunctionRunner {
	run(metadata: umf.ClientFunctionMetadata): Promise<void> {
		window.alert(metadata.customProperties.message);
		return Promise.resolve();
	}
}