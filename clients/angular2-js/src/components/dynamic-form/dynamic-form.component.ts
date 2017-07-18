import {
    Component,
    Input,
    OnChanges
} from '@angular/core';
import {
    FormControl,
    FormGroup,
    Validators
} from '@angular/forms';


@Component({
    selector: 'dynamic-form',
    template: require('./dynamic-form.component.html')
})
export class DynamicFormComponent implements OnChanges {
    @Input() questions:Array<string>;

    formGroup: FormGroup;
    payload: string;

    ngOnChanges() {
        this.formGroup = this.generateForm(this.questions || []);
        this.payload = '';
    }

    private generateForm(questions: Array<string>): FormGroup {
        const formControls = questions.reduce(this.generateControl, {});

        return new FormGroup(formControls);
    }

    private generateControl(controls: any) {
        

        return controls;
    }

    submit() {
        this.payload = JSON.stringify(this.formGroup.value, null, 4);
    }
}