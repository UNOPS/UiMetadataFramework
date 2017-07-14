import { FormMetadata, FormResponse } from "./ui-metadata-framework/index";
import {Injectable} from '@angular/core';
import * as axiosLib from "axios";

var axios = axiosLib.default;

export class MyApp {
	// getMetadata(formId: string): Promise<umf.FormMetadata> {
	// 	return this.http.get(`http://localhost:62790/api/form/metadata/${formId}`)
	// 	.map((response:umf.FormMetadata) => {
	// 		console.log(response);
	// 		return response;
	// 	}); 
	// } 

	getAllMetadata(): Promise<FormMetadata[]> {
        return axios.get("http://localhost:62790/api/form/metadata/").then((response: axiosLib.AxiosResponse) => {
            return <FormMetadata[]>response.data;
        });
    }
}