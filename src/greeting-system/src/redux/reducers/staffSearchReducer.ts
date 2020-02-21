
import { IStaffSearcActions } from '../actions/staffSearchActions';
import { StaffSearchEvents } from '../events/staffSearchEvents';


export interface IStaffSearchState {
    searchTerm: string,
    selectedId: string;
}

const initState: IStaffSearchState = {
    searchTerm: '',
    selectedId: ''
}

export const reducer = (state: IStaffSearchState = initState, action: IStaffSearcActions) : IStaffSearchState => {
    switch (action.type) {
            
        case StaffSearchEvents.STAFF_SELECTION_CHNAGED:
            return{...state, searchTerm: action.payload};
        case StaffSearchEvents.SEARCH_TEXT_CHANGED:
            return {...state, selectedId: action.payload};
    
        default: return state;
    }
}