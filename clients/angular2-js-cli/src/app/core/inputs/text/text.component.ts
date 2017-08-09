import { Component, OnInit, Input } from '@angular/core';
import { InputComponent } from "../input";
import { FormControl, FormGroup } from "@angular/forms";
import { InputFieldMetadata } from "uimf-core/src";

@Component({
  selector: '[app-text]',
  templateUrl: './text.component.html',
  styleUrls: ['./text.component.css']
})
export class TextComponent   {
 // @Input() model: InputFieldMetadata;
 
  config;
  group: FormGroup;
  constructor() { }

}
