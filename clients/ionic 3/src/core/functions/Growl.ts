import * as umf from '../framework/uimf-core/src/index';
import { IFunctionRunner } from '../framework/index';

export class Growl implements IFunctionRunner {
	run(metadata: umf.ClientFunctionMetadata): Promise<void> {
		window.alert(metadata.customProperties.message);
		return Promise.resolve();
	}
}