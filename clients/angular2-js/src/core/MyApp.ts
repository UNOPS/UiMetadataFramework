import { FormMetadata, FormResponse } from "./ui-metadata-framework/index";
import { Injectable } from '@angular/core';
import {
    Http,
    Response
} from '@angular/http';

export class MyApp { 
 constructor(private http: Http) {}
	getAllMetadata(): Promise<FormMetadata[]> {
        return this.http.get("http://localhost:62790/api/form/metadata/").toPromise().then(response => response.json() as FormMetadata[]);
    }
}