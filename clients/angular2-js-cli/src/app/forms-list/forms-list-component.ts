import {Component} from "@angular/core";
import { FormMetadata } from "uimf-core";
import { MetadataService } from "../../services/metadata.service";
import { Router } from '@angular/router';

@Component({
    selector:'forms-list',
    templateUrl: './forms-list-component.html'
})


export class FormsListComponent {

    myMetadata: Array<FormMetadata> = [];

    constructor(private metadataService: MetadataService, private router: Router) {
        this.metadataService.metadata
            .subscribe((metadata) => this.myMetadata = metadata);
    }

    displayForm(id: number) {
       // this.router.navigateByUrl(`/form/${id}`);
    }

}