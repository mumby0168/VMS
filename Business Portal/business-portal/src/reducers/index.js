import {combineReducers} from 'redux'
import ui from './uiReducer'
import account from './accountReducer'
import login from './loginReducer'

export default combineReducers({
    ui,
    account,
    login
})