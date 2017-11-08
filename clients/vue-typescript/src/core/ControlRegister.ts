import * as umf from "core-framework";

import { DateInputController } from "core-ui/Controllers/inputs/DateInputController";
import { NumberInputController } from "core-ui/Controllers/inputs/NumberInputController";
import { DropdownInputController } from "core-ui/Controllers/inputs/DropdownInputController";
import { BooleanInputController } from "core-ui/Controllers/inputs/BooleanInputController";
import { PaginatorInputController } from "core-ui/Controllers/inputs/PaginatorInputController";
import { MultiSelectInputController } from "core-ui/Controllers/inputs/MultiSelectInputController";
import { TypeaheadInputController } from "core-ui/Controllers/inputs/TypeaheadInputController";
import { PasswordInputController } from "core-ui/Controllers/inputs/PasswordInputController";

import { TextInput } from "core-ui/Elements/inputs/Text";
import { NumberInput } from "core-ui/Elements/inputs/Number";
import { DateInput } from "core-ui/Elements/inputs/Date";
import { DropdownInput } from "core-ui/Elements/inputs/Dropdown";
import { BooleanInput } from "core-ui/Elements/inputs/Boolean";
import { MultiSelectInput } from "core-ui/Elements/inputs/MultiSelect";
import { PasswordInput } from "core-ui/Elements/inputs/Password";

import { TextOutput } from "core-ui/Elements/outputs/Text";
import { NumberOutput } from "core-ui/Elements/outputs/Number";
import { DateTimeOutput } from "core-ui/Elements/outputs/Datetime";
import { TableOutput } from "core-ui/Elements/outputs/Table";
import { FormLink } from "core-ui/Elements/outputs/FormLink";
import { Tabstrip } from "core-ui/Elements/outputs/Tabstrip";
import { Paginator } from "core-ui/Elements/outputs/Paginator";
import { ActionList } from "core-ui/Elements/outputs/ActionList";
import { InlineForm } from "core-ui/Elements/outputs/InlineForm";
import { TextValue } from "core-ui/Elements/outputs/TextValue";

import {
	FormLogToConsole,
	BindToOutput,
	InputLogToConsole,
	OutputLogToConsole
} from "core-eventHandlers";

import { Growl } from "core-functions";

var controlRegister = new umf.ControlRegister();

controlRegister.registerInputFieldControl("text", TextInput, umf.StringInputController);
controlRegister.registerInputFieldControl("datetime", DateInput, DateInputController);
controlRegister.registerInputFieldControl("number", NumberInput, NumberInputController);
controlRegister.registerInputFieldControl("dropdown", DropdownInput, DropdownInputController);
controlRegister.registerInputFieldControl("boolean", BooleanInput, BooleanInputController);
controlRegister.registerInputFieldControl("paginator", null, PaginatorInputController);
controlRegister.registerInputFieldControl("typeahead", MultiSelectInput, TypeaheadInputController);
controlRegister.registerInputFieldControl("multiselect", MultiSelectInput, MultiSelectInputController);
controlRegister.registerInputFieldControl("password", PasswordInput, PasswordInputController);

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

// Form event handlers.
controlRegister.registerFormEventHandler("log-to-console", new FormLogToConsole());

// Input event handlers.
controlRegister.registerInputFieldEventHandler("bind-to-output", new BindToOutput());
controlRegister.registerInputFieldEventHandler("log-to-console", new InputLogToConsole());

// Output event handlers.
controlRegister.registerOutputFieldEventHandler("log-to-console", new OutputLogToConsole());

// Functions.
controlRegister.registerFunction("growl", new Growl());

export default controlRegister;