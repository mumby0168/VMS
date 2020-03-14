import * as Urls from '../names/urls'
import { get, handleErrorWithToast } from '../utils/httpClient';
import {VisitorReducerEvents} from "../reducers/visitorsReducer";


export  const getVisitorsForBusiness = (businessId) => {
    return (dispatch) => {
        dispatch({type: VisitorReducerEvents.FETCH_BUSINESS_VISITORS});
        get(`${Urls.gatewayBaseUrl}visitors/business/${businessId}`)
            .then((result) => {
                if(result.status === 200) {
                    dispatch({type: VisitorReducerEvents.FETCHED_BUSINESS_VISITORS, payload: result.data});
                }
                else if(result.status === 204) {
                    console.log("No Visitors")
                    dispatch({type: VisitorReducerEvents.FETCHED_BUSINESS_VISITORS, payload: []});
                }

            })
            .catch((err) => {
                    handleErrorWithToast(dispatch, err);
            })
    }
}