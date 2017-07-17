import { Component, ViewEncapsulation } from '@angular/core';

import { MetadataService } from "../../services/metadata.service";
import { FormService } from '../../services/form.service';
import { RestService } from '../../services/rest.service';

import { FormData, Question } from '../../models';
import { FormMetadata } from "../../core/ui-metadata-framework/index";


@Component({
    selector: 'dynamic-form-app',
    template: require('./app.component.html'),
    styles: [ require('./app.component.scss') ]
})
export class AppComponent {
    forms: FormData[] = null;
    metadata: FormMetadata[] = null;
    selectedForm: FormData = null;

    constructor(private metadataService: MetadataService, private restService: RestService) {
         restService.getAllMetadata().then((metadata: FormMetadata[]) => {
            this.metadataService.setMetadata(metadata);
        });
    }
}