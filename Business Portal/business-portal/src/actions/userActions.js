import {get} from '../utils/httpClient';
import * as urls from '../names/urls'

export function getUserInfo() {
    return (dispatch) => {
        dispatch({type: "FETCHING_USER_INFO"});
        get(`${urls.gatewayBaseUrl}users/info/`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_USER_INFO", payload: res.data});
            }
        })
        .catch((err) => {
            if(err.response === undefined || null) {
                dispatch({type: "REJECTED_USER_INFO", payload: {Code: "not_accesible", Reason: "Our services are currently down."}});
                return;
            }
            
            if(err.response.status === 400) {                
                console.log(err.response.data)
                dispatch({type: "REJECTED_USER_INFO", payload: err.response.data});
                return;
            }          
        });
    }
}