import { Component, OnInit, Input } from '@angular/core';
import { FormMetadata } from "uimf-core/src";
import { MetadataService } from "../../services/metadata.service";

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {
  
  myMetadata: Array<FormMetadata> = [];
  constructor(private metadataService: MetadataService) { 

  this.metadataService.metadata
            .subscribe((metadata) => this.myMetadata = metadata);
  }

  ngOnInit() {
  }

}
