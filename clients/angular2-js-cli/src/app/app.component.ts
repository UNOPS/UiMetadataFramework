import { Component } from '@angular/core';
import { MetadataService } from "../services/metadata.service";
import { RestService } from '../services/rest.service';
import { FormMetadata } from "uimf-core";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private metadataService: MetadataService, private restService: RestService) {
    restService.getAllMetadata().then((metadata: FormMetadata[]) => {
      this.metadataService.setMetadata(metadata);
    });
  }
}


