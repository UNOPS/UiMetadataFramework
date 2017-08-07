import { FormMetadata, FormResponse } from "uimf-core";
import { Injectable } from '@angular/core';
import {
    Http,
    Response
} from '@angular/http';
import { RestService } from '../services/rest.service';
export class MyApp { 
 constructor(private http: Http) {}
	getAllMetadata(): Promise<FormMetadata[]> {
        return this.http.get(RestService.url +"/metadata").toPromise().then(response => response.json() as FormMetadata[]);
    }
}