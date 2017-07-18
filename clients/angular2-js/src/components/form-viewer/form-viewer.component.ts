import {
    Component,
    OnInit
} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MetadataService } from '../../services/metadata.service';

import { FormService } from '../../services/form.service';
import { FormMetadata } from "../../core/ui-metadata-framework/index";

@Component({
    selector: 'form-viewer',
    template: require('./form-viewer.component.html'),
    styles: []
})
export class FormViewerComponent implements OnInit {
  
form:FormMetadata = null;
    constructor(private metadataService: MetadataService, private route: ActivatedRoute) {}

    ngOnInit() {
        debugger;
            this.route.params.map((param) => param['id'])
                .forEach((id: string) => this.selectForm(id));
    }

    private selectForm(id: string) {
        debugger;
        //const selectedForm = this.metadataService.metadata.value.find((form) => form.id == id);
        
           this.metadataService.metadata
            .subscribe((metadata) => this.form = metadata.find(f => f.id == id));
    }
}