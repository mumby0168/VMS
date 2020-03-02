import { fetchVisitorFormSpecAction, fetchedVisitorFormSpecAction, rejectedVisitorFormSpecAction, IVisitorDataSpecification } from '../actions/visitorFormActions'
import { gatewayClient, IFailedRequestResponse } from '../api/helpers'
import { AxiosError } from 'axios';


export const getVisitorFormSpecifications = () => {
    return async (dispatch: any) => {
        dispatch(fetchVisitorFormSpecAction(true));

        try {
            const resposne = await gatewayClient().get<IVisitorDataSpecification[]>('visitors/specs');

            dispatch(fetchVisitorFormSpecAction(false));

            if (resposne.status === 200) {
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

interface ISpec {
    fieldId: string,
    value: string;
}

export const submitVisitorForm = async (siteId: string, visitingId: string, data: IVisitorDataSpecification[]): Promise<string> => {    

    const fields: ISpec[] = []

    console.log(data);

    for (let i = 0; i < data.length; i++) {
        fields.push({
            fieldId: data[i].id,
            value: data[i].value
        });
    }

    try {
        const result = await gatewayClient().post("visitors/create", {
            siteId,
            visitingId,
            data: fields
        });
        

        if (result.status === 202) {
            return result.headers["X-Operation"]
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