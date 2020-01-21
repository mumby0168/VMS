const initialState = {
        access: {
            isOpen: false,
            loading: false,
            employee: null,
            records: [

            ]
        },
        loading: false,
        summaries: [

        ]
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

    case "OPEN_EMPLOYEE_RECORDS": return {...state, access: {...state.access, isOpen: true, employee: payload}};

    case "CLOSE_EMPLOYEE_RECORDS": return {...state, access: {...state.access, isOpen: false, employee: null}};

    case "FETCH_EMPLOYEE_RECORDS": return {...state, access: {...state, loading: true}};

    case "FETCHED_EMPLOYEE_RECORDS": return {...state, access: {...state, loading: false, records: payload}};

    case "REJECTED_EMPLOYEE_RECORDS": return {...state, access: {...state, loading: false, records: []}};

    default:
        return state
    }
}
