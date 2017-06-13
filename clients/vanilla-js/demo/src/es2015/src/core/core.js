import { $ } from "./$";
export class UmfApp {
    getMetadata(formId) {
        return $.get(`/form/metadata/${formId}`).then((response) => {
            console.log(response);
            return response;
        });
    }
    getAllMetadata() {
        return null;
    }
}
//# sourceMappingURL=core.js.map