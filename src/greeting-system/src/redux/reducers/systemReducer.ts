
import {SystemEvents} from '../events/systemEvents';
import { ISystemActions } from '../actions/systemActions';

export interface IAuth {
    jwt: string;
    refreshToken: string;
}

export interface ISystemState {
    online: boolean;
    auth: IAuth
}

const initState: ISystemState = {
    online: false,
    auth: {
        jwt: "",
        refreshToken: ""
    }
}

export const reducer = (state: ISystemState = initState, action: ISystemActions) => {
    switch(action.type) {

        case SystemEvents.AUTH_OBTAINED:
            return {...state, online: true, auth: {
                jwt: action.payload.jwt,
                refreshToken: action.payload.refreshToken
            }}

        default: return state;
    }
}