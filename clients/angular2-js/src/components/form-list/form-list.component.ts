import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MyApp } from '../../core/MyApp';

import { FormData } from '../../models';
import { FormService } from '../../services/form.service';
import { FormMetadata, FormResponse } from "../../core/ui-metadata-framework/index";
import { MetadataService } from "../../services/metadata.service";



@Component({
    selector: 'form-list',
    template: require('./form-list.component.html'),
    styles: []
})

export class FormListComponent {
    forms: Array<FormData> = [];
    myMetadata: Array<FormMetadata> = [];
    
    constructor(private metadataService: MetadataService, private router: Router) {
        this.metadataService.metadata
            .subscribe((metadata) => this.myMetadata = metadata);
    }

    displayForm(id: number) {
        //this.router.navigateByUrl(`/form/${id}`);
    }

}