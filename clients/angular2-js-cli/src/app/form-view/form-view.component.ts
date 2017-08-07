import { Component, OnInit, ElementRef, AfterViewInit, OnDestroy, ViewChild, ComponentFactoryResolver, Input } from '@angular/core';
import { FormMetadata } from "uimf-core";
import { ActivatedRoute } from "@angular/router";
import { MetadataService } from "../../services/metadata.service";
import { InputComponent } from "../core/inputs/input";
import { TextComponent } from "../core/inputs/text/text.component";
import { ControlItem } from "../core/control.item";
import { NumberComponent } from "../core/inputs/number/number.component";

@Component({
  selector: 'form-view',
  templateUrl: './form-view.component.html',
  styleUrls: ['./form-view.component.css'],
})
export class FormViewComponent implements OnInit   {

  page = {
    childrens: [
      {
        data: 'Ashraf',
        type: 'text'
      },
      {
        data: '123',
        type: 'number'
      }
    ]
  }
  form1: any;
  form: FormMetadata = null;
  constructor(private metadataService: MetadataService, private route: ActivatedRoute) {
    route.data.do(console.log).subscribe(data => this.form1 = data['form11']);
  }
  
   ngOnInit() {
    this.route.params.map((param) => param['id'])
      .forEach((id: string) => this.selectForm(id));
     
  }

  private selectForm(id: string) {
    //const selectedForm = this.metadataService.metadata.value.find((form) => form.id == id);
    this.metadataService.metadata
      .subscribe((metadata) => this.form = metadata.find(f => f.id == id));
     this.form.inputFields
  }


}

