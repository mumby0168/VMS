import {get} from '../utils/httpClient'
import * as urls from '../names/urls'

export function getPersonalAccessRecords() {   
    return (dispatch) => {
        dispatch({type: "FETCHING_PERSONAL_ACCESS_RECORDS"})
        get(`${urls.gatewayBaseUrl}users/records/`).then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_PERSONAL_ACCESS_RECORDS", payload: res.data});
            }
            else if(res.status === 204) {
                dispatch({type: "FETCHED_PERSONAL_ACCESS_RECORDS", payload: []});
            }
        })
        .catch((err) => {
            dispatch({type: "FETCHED_PERSONAL_ACCESS_RECORDS", payload: [], error: "No data could be collected."});
        })    
    }
}