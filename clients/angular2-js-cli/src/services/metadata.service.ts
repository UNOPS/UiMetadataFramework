import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/Rx';
import { FormMetadata } from "uimf-core";
import { Observable } from "rxjs/Observable";

@Injectable()
export class MetadataService {
    metadata = new BehaviorSubject<Array<FormMetadata>>([]);

    setMetadata(newMetadata: Array<FormMetadata>) {
        
        this.metadata.next(newMetadata);

    }

    getTestData() {
        return new Observable(observer => {
            observer.next(this.metadata.value.slice());
            observer.complete();
        });
    }
}