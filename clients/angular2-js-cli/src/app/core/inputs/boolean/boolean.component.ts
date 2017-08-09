import { Component, OnInit, Input } from '@angular/core';
import { InputFieldMetadata } from "uimf-core/src";
import { FormGroup } from "@angular/forms/src/model";

@Component({
  selector: 'app-boolean',
  templateUrl: './boolean.component.html',
  styleUrls: ['./boolean.component.css']
})
export class BooleanComponent   {

  //@Input() model: InputFieldMetadata;
  config;
  group: FormGroup;
  constructor() { }


}
