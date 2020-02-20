import { makeAction, IActionUnion } from "../../redux-helpers/helpers";
import { OverlayEvents } from "../events/overlayEvents";

export interface IUpdateOverlayParams {
    message: string;
    showSpinner: boolean;
    isOpen: boolean;
    icon: IconType;
    clearButton: boolean;
}

export enum IconType {
    NONE = 0,
    TICK = 1,
    ERROR = 2,
}

export const closeOverlay = (): IUpdateOverlayParams => { 
    return {
        isOpen: false,
        showSpinner: false,
        message: '',
        icon: IconType.NONE,
        clearButton: false           
    }    
}

export const openOverlay = (message: string, icon: IconType, loading: boolean = false, clearButton: boolean = false) : IUpdateOverlayParams => {
    return {
        message: message,
        icon: icon,
        isOpen: true,
        showSpinner: loading,
        clearButton: clearButton,
    }
}


export const updateOverlayAction = makeAction<OverlayEvents.UPDATE, IUpdateOverlayParams>(OverlayEvents.UPDATE);


const actions = {
    updateOverlayAction
};

export type IOverlayActions = IActionUnion<typeof actions>;