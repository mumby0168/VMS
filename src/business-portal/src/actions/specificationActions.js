import {get, handleErrorWithToast, postCallback, deleteCallback, showToast} from "../utils/httpClient";
import * as urls from '../names/urls'
import {showSiteSpinner} from "./uiActions";

export function getBusinessSpecifications() {
    return (dispatch) => {
        dispatch({type: "FETCH_SPECS"});
        get(`${urls.gatewayBaseUrl}visitors/specs`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_SPECS", payload: res.data});
            }
        })
        .catch((err) => 
        {
            dispatch({type: "FETCHED_SPECS", payload: []});
            handleErrorWithToast(dispatch, err);
        });
    }
}

export function clearAddSpecForm() {
    return {
        type: "CLEAR_ADD_SPEC"
    }
}


export function getDeprecatedSpecifications() {
    return (dipatch) => {
        
    }
}

export function showAdd() {
    return {
        type: "OPEN_ADD_SPEC"
    }
}

export function closeAdd() {
    return {
        type: "CLOSE_ADD_SPEC"
    }
}

export function updateForm(value, key) {
    return {
        type: "UPDATE_ADD_SPEC_FORM",
        payload: {
            value: value,
            key: key
        }
    }
}


export function createDataSpec(dispatch, label, validationMessage, validationCode) {
    postCallback(`${urls.gatewayBaseUrl}visitors/spec/create`, {
        label,
        validationCode,
        validationMessage
        },
        dispatch,
        () => (dispatch) => {
            dispatch(showSiteSpinner("Creating your data specification ..."))
        }, (op) => (dispatch) => {
            dispatch(showToast("Successfully created data specification."));
            dispatch(getBusinessSpecifications());
            dispatch(clearAddSpecForm());
            dispatch(closeAdd());
        }, (op) => (dispatch) => {
            dispatch(showToast(op.reason, true));
        });
}


export function deprecateDataSpec(dispatch, id) {
    deleteCallback(`${urls.gatewayBaseUrl}visitors/spec/deprecate`, {
        id,       
    }, dispatch,() => (dispatch) => {
        dispatch(showSiteSpinner("Deprecating your data specification ..."))
    }, (op) => (dispatch) => {
        dispatch(showToast("Successfully deprecated data specification."));
        dispatch(getBusinessSpecifications());
    }, (op) => (dispatch) => {
        dispatch(showToast(op.reason, true));
    });
}


export function getVaidationOptions() {
    return (dispatch) => {
        dispatch({type: "FETCH_VALIDATORS"});
        get(`${urls.gatewayBaseUrl}visitors/specs/validators`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_VALIDATORS", payload: res.data});
            }
        })
    }
}


export function updateSpecOrder(id, order, dispatch) {
    postCallback(`${urls.gatewayBaseUrl}visitors/spec/reorder`, {
        entryId: id,
        order
    }, dispatch,  () => (dispatch) => {
        dispatch(showSiteSpinner("Updating your data specification order ..."))
    }, (op) => (dispatch) => {
        dispatch(showToast("Successfully updated data specification order."));
        dispatch(getBusinessSpecifications());
    }, (op) => (dispatch) => {
        dispatch(showToast(op.reason, true));
    });
}