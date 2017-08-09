import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { InputFieldMetadata } from "uimf-core/src";

@Component({
    selector: 'dynamic-form',
    //styleUrls: ['dynamic-form.component.scss'],
    template: `
     <form
      class="dynamic-form"
      [formGroup]="form"
      (ngSubmit)="submitted.emit(form.value)">
      <ng-container
        *ngFor="let field of config;"
        dynamicField
        [config]="field"
        [group]="form">
      </ng-container>
        <div 
      class="dynamic-field form-button">
      <button
        type="submit">
       submit
      </button>
    </div>
    </form>
  `
})
export class DynamicFormComponent implements OnInit {
    @Input()
    config: any[] = [];

    form: FormGroup;
    
    @Output()
    submitted: EventEmitter<any> = new EventEmitter<any>();
    constructor(private fb: FormBuilder) { }

    ngOnInit() {
        this.form = this.createGroup();
    }

    createGroup() {
        const group = this.fb.group({});
        this.config.forEach(control => group.addControl(control.label, this.createControl(control)));
        return group;
    }

    createControl(config: InputFieldMetadata) {
        const { label } = config;
        return this.fb.control({ label });
    }
}