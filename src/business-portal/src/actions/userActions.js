import {get, handleErrorWithCritical, postCallback, showToast} from '../utils/httpClient';
import * as urls from '../names/urls'
import { showSiteSpinner, hideSiteSpinner } from './uiActions';
import {clearAddSpecForm, closeAdd, getBusinessSpecifications} from "./specificationActions";

export function userInformationNotPresent() {
    return {
        type:"USER_INFO_NOT_PRESENT"
    }
}

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
                dispatch(userInformationNotPresent());
            }
        })
        .catch(err => {
            dispatch(hideSiteSpinner());
            handleErrorWithCritical(dispatch, err, "REJECTED_USER_INFO")            
        });
    }
}


export function createUser(accountId, businessId, siteId, firstName, secondName, phone, businessPhone) {
    console.log(firstName)
    return (dispatch) => {
        postCallback(`${urls.gatewayBaseUrl}users/create`, {
            accountId: accountId,
            firstName: firstName,
            secondName: secondName,
            phoneNumber: phone,
            businessPhoneNumber: businessPhone,
            basedSiteId: siteId,
            businessId: businessId
        }, dispatch, () => (dispatch) => {
            dispatch(showSiteSpinner("Saving your information ..."))
        }, (op) => (dispatch) => {
            dispatch(showToast("Successfully saved your information."));
            dispatch({type: "USER_CREATION_COMPLETE"});
            dispatch({type: "USER_CREATION_COMPLETE"});
            dispatch(getUserInfo())
        }, (op) => (dispatch) => {
            dispatch(showToast(op.reason, true));
        });
    }
}