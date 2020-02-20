import { makeAction, IActionUnion } from "../../redux-helpers/helpers";
import { OverlayEvents } from "../events/overlayEvents";

export interface IUpdateOverlayParams {
    message: string;
    showSpinner: boolean;
    isOpen: boolean;
}

export const updateOverlayAction = makeAction<OverlayEvents.UPDATE, IUpdateOverlayParams>(OverlayEvents.UPDATE);


const actions = {
    updateOverlayAction
};

export type IOverlayActions = IActionUnion<typeof actions>;