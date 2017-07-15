import { Injectable } from '@angular/core';
import {
    Http,
    Response
} from '@angular/http';

import 'rxjs/add/operator/map';

import { FormData } from '../models';
import { FormMetadata } from "../core/ui-metadata-framework/index";

@Injectable()
export class RestService {
    constructor(private http: Http) {}

    getForms() {
        return this.http.get('api/forms')
                .map((response) => {
                    const json = response.json();

                    if (response.ok) {
                        return json.data as FormData[];
                    } else {
                        return this.logError(json);
                    }
                });
    }

    getAllMetadata() {
        debugger;
        return this.http.get(`http://localhost:62790/api/form/metadata/`)
                .map((response) => {
                    const json = response.json();
                    if (response.ok) {
                        console.log(json.data);
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