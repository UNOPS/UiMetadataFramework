import { Type } from "@angular/core";

export class ControlItem {
    constructor(public component: Type<any>, public type: string) { }
}
