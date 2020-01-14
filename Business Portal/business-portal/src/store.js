import { createStore, applyMiddleware } from "redux";
import rootReducer from './reducers/index'
import thunk from 'redux-thunk'
import logger from 'redux-logger'


export const configureStore = () => {
    var middelware = applyMiddleware(thunk, logger);

    return createStore(
        rootReducer,
        middelware
    )
}