import { makeAction, IActionUnion } from "../../redux-helpers/helpers"
import { SystemEvents } from "../events/systemEvents"

export const loginSuccesful = makeAction<SystemEvents.LOGIN_SUCCESFUL, string>(SystemEvents.LOGIN_SUCCESFUL);

const actions = {
    loginSuccesful
}

export type ISystemActions = IActionUnion<typeof actions>;