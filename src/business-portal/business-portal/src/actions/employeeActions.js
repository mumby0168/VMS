import { get, handleError, handleErrorWithToast, post } from '../utils/httpClient'
import * as urls from '../names/urls'


export function openEmployeeRecords(employee) {
    return {
        type: "OPEN_EMPLOYEE_RECORDS",
        payload: employee
    }
}


export function closeEmployeeRecords() {
    return { type: "CLOSE_EMPLOYEE_RECORDS" };
}


export function getLatestEmployeeRecords() {
    return (dispatch) => {
        dispatch({ type: "FETCH_EMPLOYEE_SUMMARIES" });
        get(`${urls.gatewayBaseUrl}users/users`)
            .then((res) => {
                if (res.status === 200) {
                    dispatch({ type: "FETCHED_EMPLOYEE_SUMMARIES", payload: res.data });
                }
            })
            .catch(err => handleError("REJECTED_EMPLOYEE_SUMMARIES", dispatch, err));
    }
}


export function getEmployeeAccessRecords(employeeId) {
    return (dispatch) => {
        dispatch({ type: "FETCH_EMPLOYEE_RECORDS" });
        get(`${urls.gatewayBaseUrl}users/records/${employeeId}`)
            .then((res) => {
                if (res.status === 200) {
                    dispatch({ type: "FETCHED_EMPLOYEE_RECORDS", payload: res.data });
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

export function showAddEmployee() {
    return {
        type: "SHOW_ADD_EMPLOYEE"
    }
}

export function hideAddEmployee() {
    return {
        type: "HIDE_ADD_EMPLOYEE"
    }
}

export function showRemoveConfirmation(id) {
    return {
        type: "SHOW_CONFIRM_REMOVE_EMPLOYEE",
        payload: id
    }
}

export function hideRemoveConfirmation() {
    return {
        type: "HIDE_CONFIRM_REMOVE_EMPLOYEE"
    }
}

export function showPending() {
    return {
        type: "SHOW_PENDING"
    }
}

export function hidePending() {
    return {
        type: "HIDE_PENDING"
    }
}

export function createEmployee(email) {
    return (dispatch) => {
        dispatch({ type: "CREATE_EMPLOYEE" });
        post(`${urls.identityBaseUrl}create`, { email: email })
            .then((res) => {
                if (res.status === 200) {
                    dispatch({ type: "CREATED_EMPLOYEE" });
                    dispatch(hideAddEmployee());
                    dispatch({
                        type: "SHOW_TOAST", payload: {
                            failed: false,
                            message: `${email} will recieve an email shortly to setup there account`
                        }
                    })
                }
            })
            .catch((err) => {
                handleError("CREATE_EMPLOYEE_FAILED", dispatch, err);
            });
    }
}


export function getPendingAccounts() {
    return (dispatch) => {
        dispatch({ type: "FETCH_PENDING_ACCOUNTS" });
        get(`${urls.identityBaseUrl}pending`)
            .then((res) => {
                if (res.status === 200) {
                    dispatch({ type: "FETCHED_PENDING_ACCOUNTS", payload: res.data });
                }
            })
            .catch((err) => {

            })
    }
}


export function removePendingAccount(id) {
    return (dispatch) => {
        dispatch({ type: "REMOVE_PENDING_ACCOUNT" });
        post(`${urls.identityBaseUrl}remove/pending/${id}`, null)
            .then((res) => {
                dispatch({ type: "REMOVED_PENDING_ACCOUNT" });
                dispatch(getPendingAccounts());
                dispatch({ type: "SHOW_TOAST", payload: { failed: false, message: "Succesfully removed account." } })
            })
            .catch((err) => {
                handleErrorWithToast("REMOVE_PENDING_ACCOUNT_FAILED", dispatch, err);
            });
    }
}


export function removeAccount(id) {
    return (dispatch) => {
        post(`${urls.identityBaseUrl}remove/${id}`, null)
            .then((res) => {
                if (res.status === 200) {
                    dispatch(hideRemoveConfirmation());
                    dispatch(getLatestEmployeeRecords());
                    dispatch({ type: "SHOW_TOAST", payload: { failed: false, message: "Succesfully removed account." } })
                }
            })
            .catch((err) => {
                handleErrorWithToast(dispatch, err);
            });
    }
}