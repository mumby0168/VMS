
import { loginAction, loginSuccesfulAction, loginRejectedAction } from '../actions/setupActions'
import { identityClient, IFailedRequestResponse } from './helpers';
import { AxiosError } from 'axios';



export interface IAuthTokenReponse {
    jwt: string;
    refreshToken: string;
}


export const login = (code: number, email: string, password: string) => {
    return async (dispatch: (action: any) => void) => {
        dispatch(loginAction(true));

        try {
            const res = await identityClient.post<IAuthTokenReponse>('sign-in', {
                code: code,
                email: email,
                password: password
            });
            
            if(res.status === 200) {
                dispatch(loginSuccesfulAction(res.data));
            }

            
        } catch (error) {
            if (error && error.response) {
                const errorResponse = error as AxiosError<IFailedRequestResponse>
                if(errorResponse.response)
                {
                    dispatch(loginRejectedAction(errorResponse.response.data))
                }            
              }
        }

        

        
    }
}