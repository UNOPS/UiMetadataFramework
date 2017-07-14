import { Injectable } from '@angular/core';
import {
    Http,
    Response
} from '@angular/http';

import 'rxjs/add/operator/map';

import { FormData } from '../models';
import { FormMetadata } from "../core/ui-metadata-framework/index";
import { BehaviorSubject } from 'rxjs/Rx';

@Injectable()
export class MyAppService {
    metadata = new BehaviorSubject<Array<FormMetadata>>([]);
    constructor(private http: Http) {}

    getAllMetadata() {
        return this.http.get("http://localhost:62790/api/form/metadata/")
                .map((response) => {
                    const json = response.json();

                    if (response.ok) {
                        return json.data as FormMetadata[];
                    } else {
                        return this.logError(json);
                    }
                });
    }

    private logError(error: any) {
        console.error(error.error);
        throw error;
    }
}