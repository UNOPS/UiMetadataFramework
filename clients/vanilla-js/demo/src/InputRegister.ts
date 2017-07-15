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

var inputRegister = new umf.InputControllerRegister();
inputRegister.register("text", TextInput, umf.StringInputController);
inputRegister.register("datetime", DateInput, DateInputController);
inputRegister.register("number", NumberInput, NumberInputController);
inputRegister.register("dropdown", DropdownInput, DropdownInputController);
inputRegister.register("boolean", BooleanInput, BooleanInputController);

export default inputRegister;