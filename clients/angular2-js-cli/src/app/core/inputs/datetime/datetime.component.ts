import { Component, OnInit, Input } from '@angular/core';
import { InputFieldMetadata } from "uimf-core/src";

@Component({
  selector: 'app-datetime',
  templateUrl: './datetime.component.html',
  styleUrls: ['./datetime.component.css']
})
export class DatetimeComponent implements OnInit {
  @Input() model: InputFieldMetadata;
  constructor() { }

  ngOnInit() {
  }

}
