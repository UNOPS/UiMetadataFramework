import { FormMetadata, FormResponse } from "./ui-metadata-framework/index";
import { FormInstance } from "./FormInstance";
import * as axiosLib from "../../node_modules/axios/index.js";

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

    postForm(formInstance: FormInstance):Promise<FormResponse> {
        var missingRequiredInputs = formInstance.inputFieldValues.filter(i => i.metadata.required && i.data == null).map(i => i.metadata.id).join(", ");
        if (missingRequiredInputs.length > 0) {
            throw new Error (`Cannot post form, because some required inputs are blank (${missingRequiredInputs}).`);
        }

        return axios.post(this.postFormUrl, JSON.stringify([{
            Form: formInstance.metadata.id,
            RequestId: 1,
            InputFieldValues: formInstance.getData()
        }]), <axiosLib.AxiosRequestConfig>{
            headers: {
                "Content-Type": "application/json"
            }
        }).then((response:axiosLib.AxiosResponse) => {
            var invokeFormResponses = <InvokeFormResponse[]>response.data;
            return invokeFormResponses[0].data;
        }).catch((error:axiosLib.AxiosError) => {
            alert(error.response.data.error);
        });
    }
}

class InvokeFormResponse
{
    public data:FormResponse;
    public requestId:string;
}