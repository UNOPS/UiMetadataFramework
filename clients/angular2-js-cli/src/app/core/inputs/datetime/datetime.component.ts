import { Component, OnInit, Input } from '@angular/core';
import { InputFieldMetadata } from "uimf-core/src";
import { FormGroup } from "@angular/forms/src/model";

@Component({
  selector: 'app-datetime',
  templateUrl: './datetime.component.html',
  styleUrls: ['./datetime.component.css']
})
export class DatetimeComponent implements OnInit {
 // @Input() model: InputFieldMetadata;
  config;
  group: FormGroup;
  constructor() { }

  ngOnInit() {
  }

}
