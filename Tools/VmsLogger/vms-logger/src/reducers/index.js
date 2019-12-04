import { combineReducers } from 'redux';
import logs from './logsReducer';
import test from  './testReducer';

export default combineReducers({
      logs,
      test
});