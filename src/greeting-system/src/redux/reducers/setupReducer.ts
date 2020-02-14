
import { ISystemActions } from '../actions/systemActions';
import { ISetupAction } from '../actions/setupActions';
import { SetupEvents } from '../events/setupEvents';
import { act } from 'react-dom/test-utils';


export interface ISetupState {
    loading: boolean;
    code: string;
    email: string;
    password: string;
    errorCode: string;
    errorMessage: string;
}

const initState: ISetupState = {
    code: "",
    email: "",
    password: "",
    loading: false,
    errorCode: "",
    errorMessage: ""
}

export const reducer = (state: ISetupState = initState, action: ISetupAction) => {
    switch (action.type) {

        case SetupEvents.LOGIN:
            return { ...state, loading: action.payload }

        case SetupEvents.LOGIN_SUCCESFUL:
            return { ...state, loading: false }

        case SetupEvents.LOGIN_REJECTED:
            return { ...state, loading: false, errorCode: action.payload.Code, errorMessage: action.payload.Reason }

        case SetupEvents.LOGIN_FORM_UPDATED:

            return { ...state, email: action.payload.email, password: action.payload.password, code: action.payload.code }

        default: return state;
    }
}