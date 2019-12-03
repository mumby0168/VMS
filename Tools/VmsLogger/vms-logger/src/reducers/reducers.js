import { combineReducers } from 'redux';
import simpleReducer from './simpleReducer';

export const reducers = () => {
  return combineReducers(simpleReducer);
};