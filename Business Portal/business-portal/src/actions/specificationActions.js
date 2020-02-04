import { get, handleErrorWithToast, postCallback } from "../utils/httpClient";
import * as urls from '../names/urls'

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
    }, "Succesfully created data specification", dispatch, "Creating data specification ...", 
    () => {
        dispatch(getBusinessSpecifications());
        dispatch(clearAddSpecForm());
        dispatch(closeAdd());
    })
}