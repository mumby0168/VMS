import { get } from '../utils/httpClient'
import * as urls from '../names/urls'


export function getSiteSummaries() {
    return (dispatch) => {
        dispatch({type: "FETCH_SITE_SUMMARIES"});
        get(`${urls.gatewayBaseUrl}sites/all`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_SITE_SUMMARIES", payload: res.data});
            }
        })
        .catch((err) => {
            dispatch({type: "REJECTED_SITE_SUMMARIES"});
        });
    }
}


export function getSiteAvailability(siteId) {
    return (dispatch) => {
        dispatch({type: "FETCH_SITE_AVAILABILITY"});
        get(`${urls.gatewayBaseUrl}sites/availability/${siteId}`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_SITE_AVAILABILITY", payload: res.data});
            }
        })
        .catch((err) => {
            dispatch({type: "REJECTED_SITE_AVAILABILITY"});
        });
    }
}