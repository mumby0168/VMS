import { get, handleErrorWithCritical } from "../utils/httpClient";
import * as urls from "../names/urls"
import { hideSiteSpinner } from "./uiActions";

export function addOperation(operation) {
    return {
        type: "OPERATION_PUSHED",
        payload: operation
    };
}

export function addOperationHandler(id, func) {
    return {
        type: "HANDLE_ADDED",
        payload: {
            id: id,
            action: func
        }
    }
}

export function signalRConnectionUpdate(state) {
    return {
        type: "SIGNALR_CONNECTION_UPDATED",
        payload: state
    }
}


export function getOperationStatus(handler) {
    return (dispatch) => {        
        get(`${urls.gatewayBaseUrl}operations/${handler.id}`)
        .then((res) => {
            if(res.status === 200) {
                console.log("Got OK res from HTTP Operations endpoint.");
                if(res.data.state === "complete") {
                    dispatch(handler.action.onCompletion(res.data))
                }
                else {
                    dispatch(handler.action.onFailure(res.data));
                }
            }
    })
        .catch(err => {
            dispatch(hideSiteSpinner());
            handleErrorWithCritical(dispatch, err, "REJECTED_OPERATION");
        })
    }
}