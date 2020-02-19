import { makeAction, IActionUnion } from "../../redux-helpers/helpers";
import {StaffKeypadEvents} from '../events/staffKeypadEvents'

export const updateCodeAction = makeAction<StaffKeypadEvents.UPDATE_CODE, string>(StaffKeypadEvents.UPDATE_CODE);


const actions = {
    updateCodeAction
}

export type IStaffKeypadActions = IActionUnion<typeof actions>;