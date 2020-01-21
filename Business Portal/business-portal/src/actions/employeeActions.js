import {get, handleError, handleErrorWithToast} from '../utils/httpClient'
import * as urls from '../names/urls'


export function openEmployeeRecords(employee) {
    return {
        type: "OPEN_EMPLOYEE_RECORDS",
        payload: employee
    }
}


export function closeEmployeeRecords() {
    return {type: "CLOSE_EMPLOYEE_RECORDS"};
}


export function getLatestEmployeeRecords() {
    return (dispatch) => {
        dispatch({type: "FETCH_EMPLOYEE_SUMMARIES"});    
        get(`${urls.gatewayBaseUrl}users/users`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_EMPLOYEE_SUMMARIES", payload: res.data});
            }
        })
        .catch(err => handleError("REJECTED_EMPLOYEE_SUMMARIES", dispatch, err));
    }
}


export function getEmployeeAccessRecords(employeeId) {
    return (dispatch) => {
        dispatch({type: "FETCH_EMPLOYEE_RECORDS"});    
        get(`${urls.gatewayBaseUrl}users/records/${employeeId}`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_EMPLOYEE_RECORDS", payload: res.data});
            }
            else {
                console.log("failed");
            }
        })
        .catch(err => {
            dispatch(closeEmployeeRecords());
            handleErrorWithToast(dispatch, err)
        });
    }
}