import {IconType, IOverlayActions} from '../actions/overlayActions';
import {OverlayEvents} from '../events/overlayEvents';
import {IVisitorFormActions} from "../actions/visitorFormActions";
import {VisitorFormEvents} from "../events/visitorFormEvents";


export interface IBackdropState {
    open: boolean;
    message: string;
    showSpinner: boolean;
    iconType: IconType;
    clearButton: boolean;
}

const initState: IBackdropState = {
    open: false,
    showSpinner: false,
    iconType: IconType.NONE,
    message: '',
    clearButton: false
}

export const reducer = (state: IBackdropState = initState, action: IOverlayActions | IVisitorFormActions) => {
    switch (action.type) {
    
        case OverlayEvents.UPDATE:
            return {...state, open: action.payload.isOpen, message: action.payload.message, showSpinner: action.payload.showSpinner, iconType: action.payload.icon, clearButton: action.payload.clearButton};

        case VisitorFormEvents.FETCH_SPEC:
            return {...state, open: true, message: 'Loading Specs', showSpinner: true};

        case VisitorFormEvents.FETCHED_SPEC:
            return initState;

        case VisitorFormEvents.REJECTED_SPEC:
            return initState;
    
        default: return state;
    }
}