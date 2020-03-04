import { makeAction, IActionUnion } from "../../redux-helpers/helpers"
import { SystemEvents } from "../events/systemEvents"
import { IAuthTokenReponse } from "../api/identity";
import { ISite } from "../reducers/systemReducer";

export enum SystemViews {
    INIT_SIGN_IN = 0,
    STAFF_SELECT = 1,
    STAFF_KEYPAD = 2,
    VISITOR_FORM = 3,
    VISITOR_OUT = 4
}

export const authObtained = makeAction<SystemEvents.AUTH_OBTAINED, IAuthTokenReponse>(SystemEvents.AUTH_OBTAINED);

export const siteFetchedAction = makeAction<SystemEvents.SITE_FETCHED, ISite>(SystemEvents.SITE_FETCHED)

export const viewChangedAction = makeAction<SystemEvents.VIEW_CHANGED, SystemViews>(SystemEvents.VIEW_CHANGED)

const actions = {
    authObtained,
    siteFetchedAction,
    viewChangedAction
}

export type ISystemActions = IActionUnion<typeof actions>;