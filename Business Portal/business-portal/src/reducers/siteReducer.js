const initialState = {
    loading: false,
    summaries: [

    ],
    availaiblity: null
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

    case "FETCH_SITE_SUMMARIES":
        return { ...state, loading: true}

    case "FETCHED_SITE_SUMMARIES":
        return { ...state, loading: false, summaries: payload}

    case "REJECTED_SITE_SUMMARIES":
        return { ...state }

    case "FETCH_SITE_AVAILABILITY":
        return { ...state, loading: true}

    case "FETCHED_SITE_AVAILABILITY":
        return { ...state, loading: false, availaiblity: payload}

    case "REJECTED_SITE_AVAILABILITY":
        return { ...state, loading: false }

    default:
        return state
    }
}
