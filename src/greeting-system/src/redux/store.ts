import { combineReducers, createStore } from "redux";

const reducers = combineReducers({

})

export type IAppState = ReturnType<typeof reducers>

export const store = createStore(reducers);