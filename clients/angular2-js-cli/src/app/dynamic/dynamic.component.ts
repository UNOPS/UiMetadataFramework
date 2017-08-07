import { Component, Input } from '@angular/core';


@Component({
  selector: 'cbx-comp',
  template: `CheckboxComponent {{ model.data }}`
})
export class CheckboxComponent {
  @Input() model: any;
}

@Component({
  selector: 'cbx-list-comp',
  template: `CheckboxListComponent {{ model.data }}`
})
export class CheckboxListComponent {
  @Input() model: any;
}

@Component({
  selector: 'datepicker-comp',
  template: `DatePickerComponent {{ model.data }}`
})
export class DatePickerComponent {
  @Input() model: any;
}
