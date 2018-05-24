import * as umf from '../../src/core/framework/index';

import { TextInput } from '../pages/ui/inputs/Text';
import { NumberInput } from "../pages/ui/inputs/Number/Number";
import { DateInput } from "../pages/ui/inputs/Date/Date";
import { DropdownInput } from "../pages/ui/inputs/Dropdown/Dropdown";
import { BooleanInput } from "../pages/ui/inputs/Boolean/Boolean";
import { MultiSelectInput } from "../pages/ui/inputs/MultiSelect/MultiSelect";
import { PasswordInput } from "../pages/ui/inputs/Password/Password";

import { TextOutput } from "../pages/ui/outputs/Text/Text";
import { NumberOutput } from "../pages/ui/outputs/Number/Number";
import { DateTimeOutput } from "../pages/ui/outputs/Datetime/Datetime";
import { TableOutput } from "../pages/ui/outputs/Table/Table";
import { FormLink } from "../pages/ui/outputs/FormLink/FormLink";
import { Tabstrip } from "../pages/ui/outputs/Tabstrip/Tabstrip";
import { Paginator } from "../pages/ui/outputs/Paginator/Paginator";
import { ActionList } from "../pages/ui/outputs/ActionList/ActionList";
import { InlineForm } from "../pages/ui/outputs/InlineForm/InlineForm";
import { TextValue } from "../pages/ui/outputs/TextValue/TextValue";

import { Growl } from "../core/functions/index";
import { DateInputController } from "../pages/ui/inputs/DateInputController";
import { NumberInputController } from "../pages/ui/inputs/NumberInputController";
import { DropdownInputController } from "../pages/ui/inputs/DropdownInputController";
import { BooleanInputController } from "../pages/ui/inputs/BooleanInputController";
import { PaginatorInputController } from "../pages/ui/inputs/PaginatorInputController";
import { MultiSelectInputController } from "../pages/ui/inputs/MultiSelectInputController";
import { TypeaheadInputController } from "../pages/ui/inputs/TypeaheadInputController";
import { PasswordInputController } from "../pages/ui/inputs/PasswordInputController";



import {
	FormLogToConsole,
	BindToOutput,
	InputLogToConsole,
	OutputLogToConsole
} from "../core/eventHandlers/index";





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