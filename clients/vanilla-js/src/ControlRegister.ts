import * as umf from "core-framework";

import { DateInputController } from "core-ui/inputs/DateInputController";
import { NumberInputController } from "core-ui/inputs/NumberInputController";
import { DropdownInputController } from "core-ui/inputs/DropdownInputController";
import { BooleanInputController } from "core-ui/inputs/BooleanInputController";
import { PaginatorInputController } from "core-ui/inputs/PaginatorInputController";
import { MultiSelectInputController } from "core-ui/inputs/MultiSelectInputController";
import { TypeaheadInputController } from "core-ui/inputs/TypeaheadInputController";
import { PasswordInputController } from "core-ui/inputs/PasswordInputController";

import TextInput from "core-ui/inputs/Text";
import NumberInput from "core-ui/inputs/Number";
import DateInput from "core-ui/inputs/Date";
import DropdownInput from "core-ui/inputs/Dropdown";
import BooleanInput from "core-ui/inputs/Boolean";
import MultiSelectInput from "core-ui/inputs/MultiSelect";
import Password from "core-ui/inputs/Password";

import TextOutput from "core-ui/outputs/Text";
import NumberOutput from "core-ui/outputs/Number";
import DateTimeOutput from "core-ui/outputs/Datetime";
import TableOutput from "core-ui/outputs/Table";
import FormLink from "core-ui/outputs/FormLink";
import Tabstrip from "core-ui/outputs/Tabstrip";
import Paginator from "core-ui/outputs/Paginator";
import ActionList from "core-ui/outputs/ActionList";
import InlineForm from "core-ui/outputs/InlineForm";
import TextValue from "core-ui/outputs/TextValue";

import { BindToOutputProcessor } from "core-ui/inputProcessors/BindToOutput";

var controlRegister = new umf.ControlRegister();
controlRegister.registerInputFieldControl("text", TextInput, umf.StringInputController);
controlRegister.registerInputFieldControl("datetime", DateInput, DateInputController);
controlRegister.registerInputFieldControl("number", NumberInput, NumberInputController);
controlRegister.registerInputFieldControl("dropdown", DropdownInput, DropdownInputController);
controlRegister.registerInputFieldControl("boolean", BooleanInput, BooleanInputController);
controlRegister.registerInputFieldControl("paginator", null, PaginatorInputController);
controlRegister.registerInputFieldControl("typeahead", MultiSelectInput, TypeaheadInputController);
controlRegister.registerInputFieldControl("multiselect", MultiSelectInput, MultiSelectInputController);
controlRegister.registerInputFieldControl("password", Password, PasswordInputController);

controlRegister.registerOutputFieldControl("text", TextOutput);
controlRegister.registerOutputFieldControl("number", NumberOutput);
controlRegister.registerOutputFieldControl("datetime", DateTimeOutput);
controlRegister.registerOutputFieldControl("table", TableOutput, { block: true });
controlRegister.registerOutputFieldControl("formlink", FormLink);
controlRegister.registerOutputFieldControl("tabstrip", Tabstrip, { alwaysHideLabel: true, block: true });
controlRegister.registerOutputFieldControl("paginated-data", Paginator, { block: true });
controlRegister.registerOutputFieldControl("action-list", ActionList, { alwaysHideLabel: true, block: true });
controlRegister.registerOutputFieldControl("inline-form", InlineForm, { alwaysHideLabel: true, block: true });
controlRegister.registerOutputFieldControl("text-value", TextValue);

controlRegister.registerInputFieldProcessor("bind-to-output", new BindToOutputProcessor());

export default controlRegister;