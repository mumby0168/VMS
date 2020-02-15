
import { SystemEvents } from '../events/systemEvents';
import { ISystemActions } from '../actions/systemActions';

export interface IAuth {
    jwt: string;
    refreshToken: string;
}

export interface ISystemState {
    online: boolean;
    auth: IAuth
    token: ITokenStructure;
}

const initState: ISystemState = {
    online: false,
    auth: {
        jwt: "",
        refreshToken: ""
    },
    token: {
        id: '',
        email: '',
        businessId: '',
        role: ''
    }
}

export const reducer = (state: ISystemState = initState, action: ISystemActions) => {
    switch (action.type) {

        case SystemEvents.AUTH_OBTAINED:
            const tokenData = processJwt(action.payload.jwt);
            console.log(tokenData);
            return {
                ...state, online: true, auth: {
                    jwt: action.payload.jwt,
                    refreshToken: action.payload.refreshToken
                    ,
                },
                token: { ...state.token, email: tokenData.email, businessId: tokenData.businessId, role: tokenData.role, id: tokenData.id }
            }

        default: return state;
    }
}

export interface ITokenStructure {
    id: string;
    email: string;
    businessId: string;
    role: string;
}

const processJwt = (jwt: string): ITokenStructure => {
    var body = jwt.split('.')[1];
    body = atob(body);
    console.log(body);
    const bodyObject = JSON.parse(body);
    return {
        id: bodyObject.nameid,
        email: bodyObject.email,
        businessId: bodyObject.businessId,
        role: bodyObject.role
    }
}