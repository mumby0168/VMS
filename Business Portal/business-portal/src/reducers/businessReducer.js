export default function reducer(state = {
    id: "",
    name: ""    
}
, action) {
    switch(action.type) {

        case "BUSINESS_INFO_FETCHED": {            
            return {...state, name: action.payload.name, id: action.payload.id};
        }

        case "BUSINESS_INFO_FETCH_FAILED": {            
            return {...state};
        }

        case "LOGOUT": { 
            return {...state, id: "", name: ""};
        }
        default: return state;
    }
}