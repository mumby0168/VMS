
import {SystemEvents} from '../events/systemEvents';
import { ISystemActions } from '../actions/systemActions';

export interface IAuth {
    jwt: string;
}

export interface ISystemState {
    online: boolean;
    auth: IAuth
}

const initState: ISystemState = {
    online: false,
    auth: {
        jwt: ""
    }
}

export const reducer = (state: ISystemState = initState, action: ISystemActions) => {
    switch(action.type) {

        case SystemEvents.LOGIN_SUCCESFUL:
            return {...state, online: true, auth: {...state.auth, jwt: action.payload}}

        default: state;
    }
}