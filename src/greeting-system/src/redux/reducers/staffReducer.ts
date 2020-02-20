
import {IStaffActions, IStaffCurrentState} from '../actions/staffActioms'
import { StaffEvents } from '../events/staffEvents';


export interface IStaffState {
    loading: boolean;
    states: IStaffCurrentState[];
    error: string;
    lastUpdated: Date
}

const initState: IStaffState = {
    loading: false,
    states: [],
    error: '',    
    lastUpdated: new Date(),
}

export const reducer = (state: IStaffState = initState, action: IStaffActions) : IStaffState => {
    switch (action.type) {
    
        case StaffEvents.FETCH_STAFF_STATE:
            return {...state, loading: true};

        case StaffEvents.FETCHED_STAFF_STATE: {
            return{...state, loading: false, lastUpdated: new Date(), states: action.payload}
        }

        case StaffEvents.FAILED_FETCHING_STAFF_STATE: {
            return {...state, loading: false, error: action.payload};
        }
    
        default: return state;
    }
}