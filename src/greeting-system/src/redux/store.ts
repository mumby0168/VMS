import { combineReducers, createStore, applyMiddleware } from "redux";
import {reducer as systemReducer} from '../redux/reducers/systemReducer'
import {reducer as setupReducer} from '../redux/reducers/setupReducer'
import {composeWithDevTools} from 'redux-devtools-extension'
import thunk from 'redux-thunk'


const middleware = applyMiddleware(thunk)

const reducers = combineReducers({
    system: systemReducer,
    setup: setupReducer
})

export type IAppState = ReturnType<typeof reducers>

export const store = createStore(reducers, composeWithDevTools(middleware));
