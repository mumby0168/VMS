import { ISetupAction } from '../actions/setupActions';
import { SetupEvents } from '../events/setupEvents';
import { IKeyValuePair } from '../common/types';


export interface ISetupState {
    loading: boolean;
    code: string;
    email: string;
    password: string;
    errorCode: string;
    errorMessage: string;
    sites: IKeyValuePair[]
    selectedSite: IKeyValuePair;
    siteSelected: boolean;
    siteConfirmed: boolean;
}

const initState: ISetupState = {
    code: "215836",
    email: "b-admin@test.com",
    password: "Test123",
    loading: false,
    errorCode: "",
    errorMessage: "",
    selectedSite: {key: '0', value: 'Select a site'},
    sites:[],
    siteSelected: false,
    siteConfirmed: false
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

        case SetupEvents.FETCH_SITES:
            return state;

        case SetupEvents.FETCHED_SITES:
            return {...state, sites: action.payload}

        case SetupEvents.SITE_SELECTION_CHANGED:
            return {...state, selectedSite: action.payload, siteSelected: true}

        case SetupEvents.SITE_SELECTION_CONFIRMED:
            return {...state, siteConfirmed: true};
        

        default: return state;
    }
}