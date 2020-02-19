import { gatewayClient, IFailedRequestResponse } from "./helpers";
import { AxiosError } from "axios";

export enum UserAccess {
    IN,
    OUT
}

export const userInOut = async (code: string, action: UserAccess): Promise<string> => {

    const url = action === UserAccess.IN ? 'users/in' : 'users/out'

    try {
        const result = await gatewayClient().post(url, {code});

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