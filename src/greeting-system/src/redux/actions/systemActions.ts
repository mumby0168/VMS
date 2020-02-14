import { makeAction, IActionUnion } from "../../redux-helpers/helpers"
import { SystemEvents } from "../events/systemEvents"
import { IAuthTokenReponse } from "../api/identity";

export const authObtained = makeAction<SystemEvents.AUTH_OBTAINED, IAuthTokenReponse>(SystemEvents.AUTH_OBTAINED);

const actions = {
    authObtained
}

export type ISystemActions = IActionUnion<typeof actions>;