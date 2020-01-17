import { get, handleError } from "../utils/httpClient";
import * as urls from '../names/urls'

export function updateTime(time) {
    return (dispatch) => {
        dispatch({type: "TIME_UPDATED", payload: time})
    }
}


export function getSiteFireList(siteId) {
    return (dispatch) => {
        dispatch({type: "FETCH_SELECTED_SITE", payload: siteId});
        get(`${urls.gatewayBaseUrl}sites/fire/${siteId}`)
        .then((res) => {            
            if(res.status === 200) {                
                dispatch({type: "FETCHED_SELECTED_SITE", payload: res.data});
            }
        }).catch((err) => handleError("REJECTED_SELECTED_SITE", dispatch, err));
    }
}