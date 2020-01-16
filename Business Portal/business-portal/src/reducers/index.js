import {combineReducers} from 'redux'
import ui from './uiReducer'
import account from './accountReducer'
import login from './loginReducer'
import operations from './operationsReducer'    
import access from './accessRecordsReducer'
import business from './businessReducer'

export default combineReducers({
    ui,
    account,
    login,
    operations,
    access,
    business
})