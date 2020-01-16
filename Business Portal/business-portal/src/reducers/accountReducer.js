

export default function reducer(state = {
    isLoggedIn: false,
    jwtToken: null,
    refreshToken: null,
    loginLoading: false,
    resetRequestLoading: false,    
    error: {
        code: null,
        reason: null
    },
    userDetails: {
        id: null,
        email: null,
        businessId: null,
        role: null
    }
}
, action) {
    switch (action.type) {

        case "LOGIN": {
            return {
                ...state, 
                loginLoading: true
            }
        }

        case "LOGIN_SUCCESFUL": {            
            return {
                ...state,
                jwtToken: action.payload.jwt,
                refreshToken: action.payload.refreshToken,
                loginLoading: false,
                isLoggedIn: true,
                userDetails: processJwt(action.payload.jwt)
            };
        }

        case "LOGIN_REJECTED": {            
            return {
                ...state, 
                loginLoading: false,
                error: {
                    code: action.payload.Code,
                    reason: action.payload.Reason
                }
            }    
        } 
        
        
        case "REQUEST_RESET_SENT": {
            return {...state, resetRequestLoading : true, error: {
                reason: null,
                code: null,
            }}
        }

        case "REQUEST_RESET_SUCCESFUL": {
            return {...state, resetRequestLoading : false}
        }        

        case "REQUEST_RESET_REJECTED": {
            return {...state, resetRequestLoading : false, error: {
                code: action.payload.Code,
                reason: action.payload.Reason
            }}
        }

        case "LOGOUT": {
            console.log("LOGOUT");
            return {
                ...state,
                isLoggedIn: false,
                userDetails: {
                    id: null,
                    email: null,
                    businessId: null,
                    role: null
                },
                jwt: null,
                refreshToken: null
            }
        }

        default:
            return state;
    }
}


const processJwt = (jwt) => {
    var body = jwt.split('.')[1];
    body = atob(body);
    body = JSON.parse(body);
    return {
        id: body.nameid,
        email: body.email,
        businessId: body.businessId,
        role: body.role
    }
}