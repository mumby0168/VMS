import { combineReducers, createStore, applyMiddleware } from "redux";
import {reducer as systemReducer} from '../redux/reducers/systemReducer'
import {reducer as setupReducer} from '../redux/reducers/setupReducer'
import {reducer as staffKeypadReducer} from '../redux/reducers/staffKeypadReducer'
import {reducer as overReducer} from './reducers/overlayReducer'
import {composeWithDevTools} from 'redux-devtools-extension'
import thunk from 'redux-thunk'
import logger from 'redux-logger'


const middleware = applyMiddleware(thunk, logger)

const reducers = combineReducers({
    system: systemReducer,
    setup: setupReducer,
    staffKeypad: staffKeypadReducer,
    overlay: overReducer
})

export type IAppState = ReturnType<typeof reducers>

export const store = createStore(reducers, composeWithDevTools(middleware));
