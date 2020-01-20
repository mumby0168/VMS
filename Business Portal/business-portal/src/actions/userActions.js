import {get, handleError} from '../utils/httpClient';
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
        .catch(err => handleError("REJECTED_USER_INFO", dispatch, err))
    }
}