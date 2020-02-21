import axios from 'axios'
import { store } from '../store'


export const identityClient = axios.create({
    baseURL: 'http://192.168.1.97:5010/api/greeting/',
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json'
    }
})

export const gatewayClient = () => axios.create({    
    baseURL: 'http://192.168.1.97:5020/gateway/api/',
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${store.getState().system.auth.jwt}`
    }
})




export interface IFailedRequestResponse {
    Code: string;
    Reason: string;
}