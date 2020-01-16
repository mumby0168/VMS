export default function reducer(state = {
    email: "",
    requestSent: false,
}, action) {
    switch(action.type) {
        
        case "RESET_REQUEST_FORM_UPDATED": {
            return {...state, email: action.payload};
        }        

        case "REQUEST_RESET_SUCCESFUL": {
            return {...state, requestSent: true}
        }  

        case "REQUEST_RESET_USER_CONFIRMED": {
            return {...state, requestSent: false};
        }

        default: return state;

    }
}