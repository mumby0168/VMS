import axios from 'axios'
import store from '../store'

export function post(url, data, authenticated = true){

    return axios.post(url, data, {
        headers: {
            'Authorization': `Bearer ${store.getState().account.jwtToken}`
        }
    })
}

export function get(url, authenticated = true) {    
    return axios.get(url, {
        headers: {
            'Authorization': `Bearer ${store.getState().account.jwtToken}`
        }
    })
}