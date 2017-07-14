import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/Rx';
import { FormMetadata } from "../core/ui-metadata-framework/index";

@Injectable()
export class MetadataService {
    metadata = new BehaviorSubject<Array<FormMetadata>>([]);

    setMetadata(newMetadata: Array<FormMetadata>) {
        this.metadata.next(newMetadata);
    }
}