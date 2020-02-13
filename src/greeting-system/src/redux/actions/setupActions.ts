import { makeAction, makeVoidAction, IActionUnion } from "../../redux-helpers/helpers";
import {SetupEvents} from "../events/setupEvents"

export const login = makeVoidAction<SetupEvents.LOGIN>(SetupEvents.LOGIN);

export const loginSuccesful = makeAction<SetupEvents.LOGIN_SUCCESFUL, string>(SetupEvents.LOGIN_SUCCESFUL);


const actions = {
    loginSuccesful
}

export type ISetupAction = IActionUnion<typeof actions>