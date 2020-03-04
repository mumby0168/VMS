

import { makeAction, IActionUnion } from  '../../redux-helpers/helpers'
import {StaffEvents} from '../events/staffEvents'

export interface IStaffCurrentState {
    initials: string;
    code: string;
    fullName: string;
    action: string;
    timeStamp: Date;
    id: string;
    userId: string;
}

export const fetchSiteStaffStateAction = makeAction<StaffEvents.FETCH_STAFF_STATE, boolean>(StaffEvents.FETCH_STAFF_STATE);

export const fetchedSiteStaffStateAction = makeAction<StaffEvents.FETCHED_STAFF_STATE, IStaffCurrentState[]>(StaffEvents.FETCHED_STAFF_STATE);

export const rejectedFetchingStaffStateAction = makeAction<StaffEvents.FAILED_FETCHING_STAFF_STATE, string>(StaffEvents.FAILED_FETCHING_STAFF_STATE);


const action = {
    fetchedSiteStaffStateAction,
    fetchSiteStaffStateAction,
    rejectedFetchingStaffStateAction
};

export type IStaffActions = IActionUnion<typeof action>;