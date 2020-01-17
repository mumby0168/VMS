const initialState = {
    lastUpdated: null,
    loading: false,
    site: {
        id: "",
        users: [

        ]
    },
    error: {
        code: null,
        reason: null
    },
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

    case "TIME_UPDATED":
        return { ...state, lastUpdated: payload }

    case "FETCH_SELECTED_SITE": return {...state, site: {id: payload, users: []}};

    case "FETCHED_SELECTED_SITE": {
        console.log(payload);
        return {
            ...state, 
            site: {
                id: payload.siteId,
                users: payload.employees
            },
            error: { code: null, reason: null }
        };
    }

    case "REJECTED_SELECTED_SITE": return {...state, error : {code: payload.code, reason: payload.reason}};



    default:
        return state
    }
}
