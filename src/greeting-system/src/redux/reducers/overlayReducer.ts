
import { IOverlayActions, IconType } from '../actions/overlayActions';
import { OverlayEvents } from '../events/overlayEvents';


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

export const reducer = (state: IBackdropState = initState, action: IOverlayActions) => {
    switch (action.type) {
    
        case OverlayEvents.UPDATE:
            return {...state, open: action.payload.isOpen, message: action.payload.message, showSpinner: action.payload.showSpinner, iconType: action.payload.icon, clearButton: action.payload.clearButton}
    
        default: return state;
    }
}