import { makeAction, makeVoidAction, IActionUnion } from "../../redux-helpers/helpers";
import {SetupEvents} from "../events/setupEvents"
import { IAuthTokenReponse } from "../api/identity";
import { IFailedRequestResponse } from "../api/helpers";

export const loginAction = makeAction<SetupEvents.LOGIN, boolean>(SetupEvents.LOGIN);

export const loginSuccesfulAction = makeAction<SetupEvents.LOGIN_SUCCESFUL, IAuthTokenReponse>(SetupEvents.LOGIN_SUCCESFUL);

export const loginRejectedAction = makeAction<SetupEvents.LOGIN_REJECTED, IFailedRequestResponse>(SetupEvents.LOGIN_REJECTED);


const actions = {
    loginSuccesfulAction,
    loginAction,
    loginRejectedAction

}

export type ISetupAction = IActionUnion<typeof actions>