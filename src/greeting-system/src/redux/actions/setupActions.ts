import { makeAction, IActionUnion } from "../../redux-helpers/helpers";
import {SetupEvents} from "../events/setupEvents"
import { IFailedRequestResponse } from "../api/helpers";
import { IKeyValuePair } from "../common/types";

export const loginAction = makeAction<SetupEvents.LOGIN, boolean>(SetupEvents.LOGIN);

export const loginSuccesfulAction = makeAction<SetupEvents.LOGIN_SUCCESFUL, boolean>(SetupEvents.LOGIN_SUCCESFUL);

export const loginRejectedAction = makeAction<SetupEvents.LOGIN_REJECTED, IFailedRequestResponse>(SetupEvents.LOGIN_REJECTED);

export const loginFormUpdate = makeAction<SetupEvents.LOGIN_FORM_UPDATED, ISetupForm>(SetupEvents.LOGIN_FORM_UPDATED);

export const fetchSitesAction = makeAction<SetupEvents.FETCH_SITES, boolean>(SetupEvents.FETCH_SITES);

export const fetchedSitesAction = makeAction<SetupEvents.FETCHED_SITES, IKeyValuePair[]>(SetupEvents.FETCHED_SITES);

export const rejectedSitesAction = makeAction<SetupEvents.REJECTED_SITES, IFailedRequestResponse>(SetupEvents.REJECTED_SITES);

export const siteSelectionChangedAction = makeAction<SetupEvents.SITE_SELECTION_CHANGED, IKeyValuePair>(SetupEvents.SITE_SELECTION_CHANGED);

export const siteSelectionConfirmed = makeAction<SetupEvents.SITE_SELECTION_CONFIRMED, boolean>(SetupEvents.SITE_SELECTION_CONFIRMED);



export interface ISetupForm {
    code: string;
    email: string;
    password: string;
}

const actions = {
    loginSuccesfulAction,
    loginAction,
    loginRejectedAction,
    loginFormUpdate,
    fetchedSitesAction,
    fetchSitesAction,
    rejectedSitesAction,
    siteSelectionChangedAction,
    siteSelectionConfirmed
}

export type ISetupAction = IActionUnion<typeof actions>