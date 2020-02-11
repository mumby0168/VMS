import { combineReducers, createStore } from "redux";
import {reducer as systemReducer} from '../redux/reducers/systemReducer'

const reducers = combineReducers({
    system: systemReducer
})

export type IAppState = ReturnType<typeof reducers>

export const store = createStore(reducers);
