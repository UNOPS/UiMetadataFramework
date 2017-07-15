import * as umf from "../../src/index";

import { DateInputController } from "./inputs/DateInputController";
import { NumberInputController } from "./inputs/NumberInputController";
import { DropdownInputController } from "./inputs/DropdownInputController";
import { BooleanInputController } from "./inputs/BooleanInputController";

import TextInput from "../svelte-components/inputs/Text";
import NumberInput from "../svelte-components/inputs/Number";
import DateInput from "../svelte-components/inputs/Date";
import DropdownInput from "../svelte-components/inputs/Dropdown";
import BooleanInput from "../svelte-components/inputs/Boolean";

import TextOutput from "../svelte-components/outputs/Text";
import NumberOutput from "../svelte-components/outputs/Number";
import DateTimeOutput from "../svelte-components/outputs/Datetime";
import TableOutput from "../svelte-components/outputs/Table";
import FormLink from "../svelte-components/outputs/FormLink";
import Tabstrip from "../svelte-components/outputs/Tabstrip";

var controlRegister = new umf.ControlRegister();
controlRegister.registerInputFieldControl("text", TextInput, umf.StringInputController);
controlRegister.registerInputFieldControl("datetime", DateInput, DateInputController);
controlRegister.registerInputFieldControl("number", NumberInput, NumberInputController);
controlRegister.registerInputFieldControl("dropdown", DropdownInput, DropdownInputController);
controlRegister.registerInputFieldControl("boolean", BooleanInput, BooleanInputController);

controlRegister.registerOutputFieldControl("text", TextOutput);
controlRegister.registerOutputFieldControl("number", NumberOutput);
controlRegister.registerOutputFieldControl("datetime", DateTimeOutput);
controlRegister.registerOutputFieldControl("table", TableOutput, { block: true });
controlRegister.registerOutputFieldControl("formlink", FormLink);
controlRegister.registerOutputFieldControl("tabstrip", Tabstrip, { alwaysHideLabel: true, block: true });

export default controlRegister;