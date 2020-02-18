import { makeAction, IActionUnion } from "../../redux-helpers/helpers"
import { SystemEvents } from "../events/systemEvents"
import { IAuthTokenReponse } from "../api/identity";
import { ISite } from "../reducers/systemReducer";

export const authObtained = makeAction<SystemEvents.AUTH_OBTAINED, IAuthTokenReponse>(SystemEvents.AUTH_OBTAINED);

export const siteFetchedAction = makeAction<SystemEvents.SITE_FETCHED, ISite>(SystemEvents.SITE_FETCHED)

const actions = {
    authObtained,
    siteFetchedAction
}

export type ISystemActions = IActionUnion<typeof actions>;