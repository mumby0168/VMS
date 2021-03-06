import { fetchSitesAction, fetchedSitesAction, rejectedSitesAction } from "../actions/setupActions"
import { gatewayClient, IFailedRequestResponse } from "./helpers";
import { IKeyValuePair } from "../common/types";
import { AxiosError } from "axios";
import { ISite } from "../reducers/systemReducer";
import { siteFetchedAction } from "../actions/systemActions";

interface IGetSitesResponse {
    id: string,
    name: string;
}


export const getSites = (businessId: string) => {    
    
    console.log('sites fetching ...')

    return async (dispatch: (action: any) => void) => {
        dispatch(fetchSitesAction);

        try {
            const result = await gatewayClient().get<IGetSitesResponse[]>(`sites/summaries/${businessId}`);

            if(result.status === 200) {
                console.log(result.data)
                var sites: IKeyValuePair[] = [];
                result.data.map((site) => [
                    sites.push({key: site.id, value: site.name})
                ]);
                dispatch(fetchedSitesAction(sites))                
            }
            else {
                console.error(result)
            }

        } catch (error) {

            console.log(error)

            if (error && error.response) {
                const errorResponse = error as AxiosError<IFailedRequestResponse>
                if(errorResponse.response)
                {
                    dispatch(rejectedSitesAction(errorResponse.response.data))
                }            
              }
              else {
                  console.error('The reponse was not handled and failed. (no error response');
                  dispatch(rejectedSitesAction({Code: 'failed', Reason: 'Our services are currently having difficulty please try again later.'}))
              }
        }

    }
}

export const getSiteInfo = (siteId: string) => {
    return async (dispatch: (action: any) => void) => {

        try {
            const result = await gatewayClient().get<ISite>(`sites/get/${siteId}`);

            if(result.status === 200) {
                dispatch(siteFetchedAction(result.data));
            }   
            else {
                console.error(result);
            }
        } catch (error) {
            console.log(error);
        }
         

    }
}