import { Injectable } from '@angular/core';
import {
    Http,
    Response
} from '@angular/http';

import { FormData } from '../models';
import { FormMetadata } from "../core/ui-metadata-framework/index";

@Injectable()
export class RestService {
     public static  url : string = "http://localhost:62790/api/form";
     constructor(private http: Http) {}
   
    getAllMetadata() : Promise<FormMetadata[]>{
        return this.http.get(RestService.url + '/metadata')
        .toPromise()
        .then(response => response.json() as FormMetadata[])
        .catch(this.logError);
    }

    private logError(error: any) {
        console.error(error.error);
        throw error;
    }
}
