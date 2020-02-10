import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import rootReducer from './reducers'
import logger from 'redux-logger'


export const configureStore = () => {

    var middleware = applyMiddleware(thunk, logger);

    return createStore(
        rootReducer,
        middleware
      );
}


