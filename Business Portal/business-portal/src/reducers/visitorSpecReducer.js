const initialState = {
    loadingSpecs: false,
    specifications: []
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

    case "FETCH_SPECS":
        return { ...state, loadingSpecs: true }

    case "FETCHED_SPECS":
        return {...state, loadingSpecs: false, specifications: payload};

    default:
        return state
    }
}
