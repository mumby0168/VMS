import {combineReducers} from 'redux'
import ui from './uiReducer'
import account from './accountReducer'
import login from './forms/loginReducer'
import operations from './operationsReducer'    
import access from './accessRecordsReducer'
import business from './businessReducer'
import requestReset from './requestResetReducer'
import user from './userReducer'
import site from './siteReducer'
import fire from './fireListReducer'
import toast from './toastReducer'
import employee from './employeesReducer'
import completeUser from './forms/completeUserReducer'
import completeAccount from './forms/completeAccountReducer'
import specs from './visitorSpecReducer'
import visitors from "./visitorsReducer";

export default combineReducers({
    ui,
    account,
    login,
    operations,
    access,
    business,
    requestReset,
    user,
    site,    
    fire,
    toast,
    employee,    
    completeUser,
    completeAccount,
    specs,
    visitors
})