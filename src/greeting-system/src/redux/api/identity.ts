
import { loginAction, loginSuccesfulAction, loginRejectedAction } from '../actions/setupActions'
import { identityClient, IFailedRequestResponse } from './helpers';
import { AxiosError } from 'axios';
import { authObtained } from '../actions/systemActions';
import { useHistory } from 'react-router';



export interface IAuthTokenReponse {
    jwt: string;
    refreshToken: string;
}




export const login = (code: string, email: string, password: string) => {


    return async (dispatch: (action: any) => void) => {
        dispatch(loginAction(true));

        try {
            const res = await identityClient.post<IAuthTokenReponse>('sign-in', {
                code: code,
                email: email,
                password: password
            });
            
            if(res.status === 200) {
                dispatch(loginSuccesfulAction(false));
                dispatch(authObtained(res.data));
            }

            
        } catch (error) {
            if (error && error.response) {
                const errorResponse = error as AxiosError<IFailedRequestResponse>
                if(errorResponse.response)
                {
                    dispatch(loginRejectedAction(errorResponse.response.data))
                }            
              }
              else {
                  console.error('The reponse was not handled and failed. (no error response');
                  dispatch(loginRejectedAction({Code: 'failed', Reason: 'Our services are currently having difficulty please try again later.'}))
              }
        }

        

        
    }
}