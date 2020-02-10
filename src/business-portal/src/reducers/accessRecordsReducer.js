export default function reducer(state = {
    loading: false,
    records: [],
    error: null
}
, action) {
    switch (action.type) {       
        
        case "FETCHING_PERSONAL_ACCESS_RECORDS": {
            return {...state, loading: true};
        }
        
        case "FETCHED_PERSONAL_ACCESS_RECORDS": {
            return {...state, loading: false, records: action.payload};
        }

        case "REJECTED_PERSONAL_ACCESS_RECORDS": {
            return {...state , loading: false, error: action.payload};
        }

        default:
            return state;
    }
}


