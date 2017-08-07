import { Component, OnInit, Input } from '@angular/core';
import { InputFieldMetadata } from "uimf-core/src";

@Component({
  selector: 'app-boolean',
  templateUrl: './boolean.component.html',
  styleUrls: ['./boolean.component.css']
})
export class BooleanComponent implements OnInit {

  @Input() model: InputFieldMetadata;
  constructor() { }

  ngOnInit() {
  }

}
