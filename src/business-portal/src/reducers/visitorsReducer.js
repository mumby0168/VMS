
export const VisitorReducerEvents = {
    FETCH_BUSINESS_VISITORS: "FETCH_BUSINESS_VISITORS",
    FETCHED_BUSINESS_VISITORS: "FETCHED_BUSINESS_VISITORS",
    REJECTED_BUSINESS_VISITORS: "REJECTED_BUSINESS_VISITORS"
}


const initialState = {
    businessVisitors: [],
    loadingBusinessVisitors: false,
    errorBusinessVisitors: "",
}



export default (state = initialState, { type, payload }) => {
    switch (type) {

        case VisitorReducerEvents.FETCH_BUSINESS_VISITORS:
            return {...state, loadingBusinessVisitors: true}
        case VisitorReducerEvents.FETCHED_BUSINESS_VISITORS:
            return {...state, businessVisitors: payload, loadingBusinessVisitors: false};
        case VisitorReducerEvents.REJECTED_BUSINESS_VISITORS:
            return {...state, errorBusinessVisitors: payload, loadingBusinessVisitors: false};

        default:
            return state
    }
}
