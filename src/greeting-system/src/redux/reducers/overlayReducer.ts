
import { IOverlayActions } from '../actions/overlayActions';
import { OverlayEvents } from '../events/overlayEvents';


export interface IBackdropState {
    open: boolean;
    message: string;
    showSpinner: boolean;
}

const initState: IBackdropState = {
    open: false,
    showSpinner: false,
    message: 'Test Message'
}

export const reducer = (state: IBackdropState = initState, action: IOverlayActions) => {
    switch (action.type) {
    
        case OverlayEvents.UPDATE:
            return {...state, open: action.payload.isOpen, message: action.payload.message, showSpinner: action.payload.showSpinner}
    
        default: return state;
    }
}