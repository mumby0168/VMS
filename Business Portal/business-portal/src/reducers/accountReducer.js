export default function reducer(state = {
    isLoggedIn: false,
    jwtToken: null,
    refreshToken: null,
    loading: false,
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
                loading: true
            }
        }

        case "LOGIN_SUCCESFUL": {                        
            return {
                ...state,
                jwtToken: action.payload.jwt,
                refreshToken: action.payload.refreshToken,
                loading: false,
                isLoggedIn: true,
                userDetails: processJwt(action.payload.jwt)
            };
        }

        case "LOGIN_REJECTED": {
            console.log(action.payload);
            return {
                ...state, 
                loading: false,
                error: {
                    code: action.payload.Code,
                    reason: action.payload.Reason
                }
            }    
        }

        case "LOGOUT": {
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