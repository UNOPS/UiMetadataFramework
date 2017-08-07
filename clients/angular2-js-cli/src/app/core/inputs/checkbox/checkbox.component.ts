import { Component, OnInit, Input } from '@angular/core';
import { InputFieldMetadata } from "uimf-core/src";

@Component({
  selector: 'app-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.css']
})
export class CheckboxComponent implements OnInit {
  @Input() model: InputFieldMetadata;
  constructor() { }

  ngOnInit() {
  }

}
