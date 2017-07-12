import { FormMetadata, FormResponse } from "./ui-metadata-framework/index";
import { FormInstance } from "./FormInstance";
import * as axiosLib from "axios";

var axios = axiosLib.default;

export class UmfServer {
    private readonly getMetadataUrl: string;
    private readonly postFormUrl: string;

	/**
	 * Creates a new instance of UmfApp.
	 */
    constructor(getMetadataUrl: string, postFormUrl: string) {
        this.getMetadataUrl = getMetadataUrl;
        this.postFormUrl = postFormUrl;
    }

    getMetadata(formId: string): Promise<FormMetadata> {
        return axios.get(`${this.getMetadataUrl}/${formId}`).then((response: axiosLib.AxiosResponse) => {
            return <FormMetadata>response.data;
        }).catch(e => {
            console.warn(`Did not find form "${formId}".`)
            return null;
        });
    }

    getAllMetadata(): Promise<FormMetadata[]> {
        return axios.get(this.getMetadataUrl).then((response: axiosLib.AxiosResponse) => {
            return <FormMetadata[]>response.data;
        });
    }

    postForm(form: string, data: any): Promise<FormResponse> {
        return axios.post(this.postFormUrl, JSON.stringify([{
            Form: form,
            RequestId: 1,
            InputFieldValues: data
        }]), <axiosLib.AxiosRequestConfig>{
            headers: {
                "Content-Type": "application/json"
            }
        }).then((response: axiosLib.AxiosResponse) => {
            var invokeFormResponses = <InvokeFormResponse[]>response.data;
            return invokeFormResponses[0].data;
        }).catch((error: axiosLib.AxiosError) => {
            alert(error.response.data.error);
        });
    }
}

class InvokeFormResponse {
    public data: FormResponse;
    public requestId: string;
}