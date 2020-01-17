import axios from 'axios'
import store from '../store'

export function post(url, data, authenticated = true){

    return axios.post(url, data, {
        headers: {
            'Authorization': `Bearer ${store.getState().account.jwtToken}`
        }
    })
}

export function get(url, authenticated = true) {    
    return axios.get(url, {
        headers: {
            'Authorization': `Bearer ${store.getState().account.jwtToken}`
        }
    })
}

export function handleError(type, dispatch, err) {

    if(err.response === undefined || null) {
        dispatch({type: type, payload: {Code: "not_accesible", Reason: "Our services are currently down."}});
        return;
    }

    if(err.response.status === 500) {
        dispatch({type: type, payload: {Code: "not_accesible", Reason: "Our services are currently down."}});
        return;
    }
    
    if(err.response.status === 400) {
        dispatch({type: type, payload: err.response.data});
        return;
    }     
}