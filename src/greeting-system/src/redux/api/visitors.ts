import {fetchVisitorFormSpecAction, fetchedVisitorFormSpecAction, rejectedVisitorFormSpecAction, IVisitorDataSpecification} from '../actions/visitorFormActions'
import {gatewayClient, IFailedRequestResponse} from  '../api/helpers'
import { AxiosError } from 'axios';


export const getVisitorFormSpecifications = () => {
    return async (dispatch: any) => {
        dispatch(fetchVisitorFormSpecAction(true));

        try {
            const resposne = await gatewayClient().get<IVisitorDataSpecification[]>('visitors/specs');

            dispatch(fetchVisitorFormSpecAction(false));

            if(resposne.status === 200) {
                dispatch(fetchedVisitorFormSpecAction(resposne.data));
            }

        } catch (error) {
            dispatch(fetchVisitorFormSpecAction(false));
            if (error && error.response) {
                const errorResponse = error as AxiosError<IFailedRequestResponse>
                if (errorResponse.response) {
                    console.error(errorResponse.response);
                    dispatch(rejectedVisitorFormSpecAction(errorResponse.response.data.Reason));
                }
            }
            else {
                console.error('The reponse was not handled and failed. (no error response)');
            }
        }
    }
}