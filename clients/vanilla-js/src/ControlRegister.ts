import * as umf from "core-framework";

import { DateInputController } from "core-ui/inputs/DateInputController";
import { NumberInputController } from "core-ui/inputs/NumberInputController";
import { DropdownInputController } from "core-ui/inputs/DropdownInputController";
import { BooleanInputController } from "core-ui/inputs/BooleanInputController";
import { PaginatorInputController } from "core-ui/inputs/PaginatorInputController";

import TextInput from "core-ui/inputs/Text";
import NumberInput from "core-ui/inputs/Number";
import DateInput from "core-ui/inputs/Date";
import DropdownInput from "core-ui/inputs/Dropdown";
import BooleanInput from "core-ui/inputs/Boolean";

import TextOutput from "core-ui/outputs/Text";
import NumberOutput from "core-ui/outputs/Number";
import DateTimeOutput from "core-ui/outputs/Datetime";
import TableOutput from "core-ui/outputs/Table";
import FormLink from "core-ui/outputs/FormLink";
import Tabstrip from "core-ui/outputs/Tabstrip";
import Paginator from "core-ui/outputs/Paginator";
import ActionList from "core-ui/outputs/ActionList";

var controlRegister = new umf.ControlRegister();
controlRegister.registerInputFieldControl("text", TextInput, umf.StringInputController);
controlRegister.registerInputFieldControl("datetime", DateInput, DateInputController);
controlRegister.registerInputFieldControl("number", NumberInput, NumberInputController);
controlRegister.registerInputFieldControl("dropdown", DropdownInput, DropdownInputController);
controlRegister.registerInputFieldControl("boolean", BooleanInput, BooleanInputController);
controlRegister.registerInputFieldControl("paginator", null, PaginatorInputController);

controlRegister.registerOutputFieldControl("text", TextOutput);
controlRegister.registerOutputFieldControl("number", NumberOutput);
controlRegister.registerOutputFieldControl("datetime", DateTimeOutput);
controlRegister.registerOutputFieldControl("table", TableOutput, { block: true });
controlRegister.registerOutputFieldControl("formlink", FormLink);
controlRegister.registerOutputFieldControl("tabstrip", Tabstrip, { alwaysHideLabel: true, block: true });
controlRegister.registerOutputFieldControl("paginated-data", Paginator, { block: true });
controlRegister.registerOutputFieldControl("action-list", ActionList, { alwaysHideLabel: true, block: true });

export default controlRegister;