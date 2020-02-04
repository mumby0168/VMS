import axios from 'axios'
import store from '../store'
import { showSiteSpinner } from '../actions/uiActions';
import { showCriticalError } from '../actions/uiActions'

export function post(url, data, authenticated = true) {

    return axios.post(url, data, {
        headers: {
            'Authorization': `Bearer ${store.getState().account.jwtToken}`,
        }
    })
}

export function showToast(message) {
    return {
        type: "SHOW_TOAST",
        payload: {
            failed: false,
            message: message,
            handled: false
        }
    }
}


export async function postCallback(url, data, toastMessage, dispatchHandle, loadingMessage = null, completionAction = null) {

    if (loadingMessage !== null) {
        dispatchHandle(showSiteSpinner(loadingMessage));
    }

    var result = await post(url, data);

    if (result.status === 202) {

        var id = result.request.getResponseHeader('x-operation');

        dispatchHandle({
            type: "HANDLE_ADDED", payload: {
                id: id,
                action: showToast(toastMessage),
                completionAction: completionAction
            }
        });
    }
}


export function get(url, authenticated = true) {
    return axios.get(url, {
        headers: {
            'Authorization': `Bearer ${store.getState().account.jwtToken}`
        }
    })
}

export function handleError(type, dispatch, err) {

    if (err.response === undefined || null) {
        dispatch({ type: type, payload: { Code: "not_accesible", Reason: "Our services are currently down." } });
        return;
    }

    if (err.response.status === 500) {
        dispatch({ type: type, payload: { Code: "not_accesible", Reason: "Our services are currently down." } });
        return;
    }

    if (err.response.status === 400) {
        dispatch({ type: type, payload: err.response.data });
        return;
    }
}


export function handleErrorWithCritical(dispatch, err, type = null) {
    if (err.response === undefined || null) {
        dispatch(showCriticalError("Our services are currently down."));
        if (type !== null) {
            dispatch({ type: type, payload: "Our services are currently down." });
        }
        return;
    }

    if (err.response.status === 500) {
        dispatch(showCriticalError("Our services are currently down."));
        if (type !== null) {
            dispatch({ type: type, payload: "Our services are currently down." });
        }
        return;
    }

    if (err.response.status === 400) {
        dispatch(showCriticalError(err.response.data.Reason));
        if (type !== null) {
            dispatch({ type: type, payload: err.response.data.Reason });
        }
        return;
    }
}

export function handleErrorWithToast(dispatch, err) {
    if (err.response === undefined || null) {
        dispatch({ type: "SHOW_TOAST", payload: { failed: true, message: "Our services are currently down." } });
        return;
    }

    if (err.response.status === 500) {
        dispatch({ type: "SHOW_TOAST", payload: { failed: true, message: "Our services are currently down." } });
        return;
    }

    if (err.response.status === 400) {
        dispatch({ type: "SHOW_TOAST", payload: { failed: true, message: err.response.data.Reason } });
        return;
    }
}