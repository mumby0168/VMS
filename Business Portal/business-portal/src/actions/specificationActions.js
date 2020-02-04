import { get, handleErrorWithToast } from "../utils/httpClient";
import * as urls from '../names/urls'

export function getBusinessSpecifications() {
    return (dispatch) => {
        dispatch({type: "FETCH_SPECS"});
        get(`${urls.gatewayBaseUrl}visitors/specs`)
        .then((res) => {
            if(res.status === 200) {
                dispatch({type: "FETCHED_SPECS", payload: res.data});
            }
        })
        .catch((err) => 
        {
            dispatch({type: "FETCHED_SPECS", payload: []});
            handleErrorWithToast(dispatch, err);
        });
    }
}