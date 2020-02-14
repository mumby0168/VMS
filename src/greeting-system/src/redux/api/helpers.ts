import axios from 'axios'


export const identityClient = axios.create({
    baseURL: 'http://localhost:5010/api/greeting/',
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json'
    }
})

export const gatewayClient = axios.create({
    responseType: 'json',
    headers: {
        'Content-Type': 'application/json'
    }
})


export interface IFailedRequestResponse {
    Code: string;
    Reason: string;
}