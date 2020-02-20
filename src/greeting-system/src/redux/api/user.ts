import { gatewayClient, IFailedRequestResponse } from "./helpers";
import { AxiosError } from "axios";
import {rejectedFetchingStaffStateAction, IStaffCurrentState, fetchedSiteStaffStateAction, fetchSiteStaffStateAction} from '../actions/staffActioms'

export enum UserAccess {
    IN,
    OUT
}

export const userInOut = async (code: string, action: UserAccess, siteId: string): Promise<string> => {

    const url = action === UserAccess.IN ? 'users/in' : 'users/out'

    try {
        const result = await gatewayClient().post(url, {code, siteId});

        if (result.status === 202) {
            return result.headers['X-Operation'];
        }
        else {
            console.error(result)
        }
    }
    catch (error) {

        if (error && error.response) {
            const errorResponse = error as AxiosError<IFailedRequestResponse>
            if (errorResponse.response) {
                console.error(errorResponse.response.data);
            }
        }
        else {
            console.error('The reponse was not handled and failed. (no error response');
        }
    }
    return "";
}


export const getStaffState = (siteId: string) => {
    return async (dispatch: any) => {

        dispatch(fetchSiteStaffStateAction(true));

        try {            
            const result = await gatewayClient().get<IStaffCurrentState[]>(`users/site-state/${siteId}`)

            if(result.status === 200) {
                dispatch(fetchedSiteStaffStateAction(result.data));
            }

        } catch (error) {
            if (error && error.response) {
                const errorResponse = error as AxiosError<IFailedRequestResponse>
                if (errorResponse.response) {
                    console.error(errorResponse.response);
                    dispatch(rejectedFetchingStaffStateAction(errorResponse.response.data.Reason));
                }
            }
            else {
                console.error('The reponse was not handled and failed. (no error response');
            }
        }
    }
}