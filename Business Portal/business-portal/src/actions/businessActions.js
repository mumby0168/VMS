import {get} from '../utils/httpClient'
import * as urls from '../names/urls'


export function getBusinessInfo(id) {
    return (dispatch) => {
        get(`${urls.gatewayBaseUrl}businesses/info/${id}`)
        .then((res) => {
            //OK
            if(res.status === 200) {
                dispatch({type: "BUSINESS_INFO_FETCHED", payload: res.data});
            }
            //NoContent
            else if(res.status === 204) {
                dispatch({type: "BUSINESS_INFO_FETCH_FAILED", payload: null});
            }
        })
        .catch((err) => {

        });
    }
}