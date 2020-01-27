export default function(state = {
    email: "",
    password: ""
}, action)
{
    switch(action.type) { 
        
        case "LOGIN_UPDATED": {
            return {...state, email: action.payload.email, password: action.payload.password};
        }

        default: return state;
    }
}