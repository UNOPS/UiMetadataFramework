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

    getAllMetadata() : Promise<FormMetadata[]>{
        return this.http.get('http://localhost:62790/api/form/metadata/')
        .toPromise()
        .then(response => response.json() as FormMetadata[])
        .catch(this.logError);
    }

    private logError(error: any) {
        console.error(error.error);
        throw error;
    }
}