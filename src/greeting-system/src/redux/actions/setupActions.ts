import { makeAction, makeVoidAction, IActionUnion } from "../../redux-helpers/helpers";
import {SetupEvents} from "../events/setupEvents"
import { IAuthTokenReponse } from "../api/identity";
import { IFailedRequestResponse } from "../api/helpers";

export const loginAction = makeAction<SetupEvents.LOGIN, boolean>(SetupEvents.LOGIN);

export const loginSuccesfulAction = makeAction<SetupEvents.LOGIN_SUCCESFUL, boolean>(SetupEvents.LOGIN_SUCCESFUL);

export const loginRejectedAction = makeAction<SetupEvents.LOGIN_REJECTED, IFailedRequestResponse>(SetupEvents.LOGIN_REJECTED);

export const loginFormUpdate = makeAction<SetupEvents.LOGIN_FORM_UPDATED, ISetupForm>(SetupEvents.LOGIN_FORM_UPDATED);

export interface ISetupForm {
    code: string;
    email: string;
    password: string;
}

const actions = {
    loginSuccesfulAction,
    loginAction,
    loginRejectedAction,
    loginFormUpdate
}

export type ISetupAction = IActionUnion<typeof actions>