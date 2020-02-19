
import {IStaffKeypadActions} from '../actions/staffKeypadActions'
import { StaffKeypadEvents } from '../events/staffKeypadEvents';


export interface IStaffKeypadState {
    staffCode: string;
}

const initState: IStaffKeypadState = {
    staffCode: ""
}

export const reducer = (state: IStaffKeypadState = initState, action: IStaffKeypadActions) => {
    switch (action.type) {
        
        case StaffKeypadEvents.UPDATE_CODE:
            return {...state, staffCode: action.payload}
    
        default: return state;
    }
}