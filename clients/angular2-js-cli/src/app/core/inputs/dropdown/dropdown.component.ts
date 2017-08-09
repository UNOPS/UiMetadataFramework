import { Component, OnInit } from '@angular/core';
import { FormGroup } from "@angular/forms/src/model";

@Component({
  selector: 'app-dropdown',
  templateUrl: './dropdown.component.html',
  styleUrls: ['./dropdown.component.css']
})
export class DropdownComponent   {
  config;
  group: FormGroup;
  constructor() { }

  ngOnInit() {
  }

}
