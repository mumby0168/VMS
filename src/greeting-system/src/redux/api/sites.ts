import { fetchSitesAction, fetchedSitesAction, rejectedSitesAction } from "../actions/setupActions"
import { gatewayClient, IFailedRequestResponse } from "./helpers";
import { IKeyValuePair } from "../common/types";
import { AxiosError } from "axios";

interface IGetSitesResponse {
    id: string,
    name: string;
}


export const getSites = (businessId: string) => {
    
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

        } catch (error) {
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