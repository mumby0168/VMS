import {StaffSearchEvents} from '../events/staffSearchEvents'
import {makeAction, IActionUnion} from '../../redux-helpers/helpers'

export const updateSearchTermAction = makeAction<StaffSearchEvents.SEARCH_TEXT_CHANGED, string>(StaffSearchEvents.SEARCH_TEXT_CHANGED)

export const updateSelectionStaff = makeAction<StaffSearchEvents.STAFF_SELECTION_CHNAGED, string>(StaffSearchEvents.STAFF_SELECTION_CHNAGED);

const actions = {
    updateSearchTermAction,
    updateSelectionStaff
}

export type IStaffSearcActions = IActionUnion<typeof actions>;