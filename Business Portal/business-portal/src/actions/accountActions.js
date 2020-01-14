import axios from 'axios'
import * as Urls from '../names/urls'


export function login(username, password) {
    return (dispatch) => {        
        dispatch({type: "LOGIN"})
        console.log(username);
        axios.post(Urls.identityBaseUrl + "sign-in", {
            email: username,
            password: password
        }).then((res) => {
            if(res.status === 200) {
                dispatch({type: "LOGIN_SUCCESFUL", payload: res.data});
            }            
        })
        .catch((err) => {
            if(err.status === undefined || null) {
                dispatch({type: "LOGIN_REJECTED", payload: {Code: "not_accesible", Reason: "Our services are currently down."}});
                return;
            }
            
            if(err.response.status === 400) {                
                console.log(err.response.data)
                dispatch({type: "LOGIN_REJECTED", payload: err.response.data});
                return;
            }                        
        })
        
    }
}


export function loginFormUpdated(username, password) {
    return (dispatch) => {
        dispatch({type: "LOGIN_UPDATED", payload: {email: username, password: password}})
    }
}

export function logout() {
    return {type: "LOGOUT"}
}