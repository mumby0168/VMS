
import { ISystemActions } from '../actions/systemActions';
import { ISetupAction } from '../actions/setupActions';
import { SetupEvents } from '../events/setupEvents';


export interface ISetupState {
    loading: boolean;
    code: string;
    email: string;
    password: string;
}

const initState: ISetupState = {
    code: "" ,
    email: "",
    password: "",
    loading: false,
}

export const reducer = (state: ISetupState = initState, action: ISetupAction) => {
    switch(action.type) {

        case SetupEvents.LOGIN:


        default: return state;
    }
}