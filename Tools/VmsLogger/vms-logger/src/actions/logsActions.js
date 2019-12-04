import axios from 'axios';

export function fetchLogs() {
    return function(dispatch) {
        dispatch({type: "FETCH_LOGS"});  
        axios.get('http://localhost:5016/api/logs')
        .then((res) => {            
            dispatch({type: "LOGS_FETCHED", payload: res.data});
        })
        .catch((err) => {
            dispatch({type: "FETCHED_LOGS_REJECTED", payload: err});
        });
    }
}