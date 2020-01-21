import {get, handleError} from '../utils/httpClient';
import * as urls from '../names/urls'
import { showSiteSpinner, hideSiteSpinner } from './uiActions';

export function getUserInfo() {
    return (dispatch) => {
        dispatch({type: "FETCHING_USER_INFO"});
        dispatch(showSiteSpinner("Personalizing your experience ..."));
        get(`${urls.gatewayBaseUrl}users/info/`)
        .then((res) => {
            if(res.status === 200) {
                dispatch(hideSiteSpinner());
                dispatch({type: "FETCHED_USER_INFO", payload: res.data});            
            }
        })
        .catch(err => {
            dispatch(hideSiteSpinner());
            handleError("REJECTED_USER_INFO", dispatch, err)            
        });
    }
}