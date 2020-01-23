import {get, handleErrorWithCritical} from '../utils/httpClient';
import * as urls from '../names/urls'
import { showSiteSpinner, hideSiteSpinner, showCriticalError } from './uiActions';

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
            else if(res.status === 204) {
                dispatch(hideSiteSpinner());
                dispatch(showCriticalError("Your personalized user settings could not be retreived"));
            }
        })
        .catch(err => {
            dispatch(hideSiteSpinner());
            handleErrorWithCritical(dispatch, err, "REJECTED_USER_INFO")            
        });
    }
}