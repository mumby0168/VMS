import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers'
import logger from 'redux-logger'
import logsReducer from './reducers/logsReducer';


export const configureStore = () => {

    var middleware = applyMiddleware(thunk, logger);

    return createStore(
        rootReducer,
        middleware
      );
}


