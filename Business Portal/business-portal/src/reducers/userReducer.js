const initialState = {
    isAvailabile: true,
    loading: false,
    id: null,
    firstName: "",
    secondName: "",
    initials: "",
    basedSiteId: ""
}

export default (state = initialState, { type, payload }) => {
    switch (type) {    

    case "FETCHING_USER_INFO":
        return { ...state, loading: true}
    case "FETCHED_USER_INFO":
        return {
            ...state, 
            loading: false,
            id: payload.id, 
            firstName: payload.firstName, 
            secondName: payload.secondName, 
            initials: extractInitials(payload.firstName, payload.secondName),
            basedSiteId: payload.basedSiteId,
        };
    case "REJECTED_USER_INFO":
        return {...state, loading: false};


    case "USER_INFO_NOT_PRESENT": {
        return {...state, isAvailabile: false};
    }

    default:
        return state
    }
}

const extractInitials = (first, second) => {        
    return first.toString()[0] + second.toString()[0];
}
