const initialState = {
    access: {
        isOpen: false,
        loading: false,
        employee: null,
        records: [

        ]
    },
    addEmployee: {
        isOpen: false,
        loading: false,
        errorMessage: "",
    },
    pending: {
        accounts: [],
        isOpen: false,
        loading: false
    },
    remove: {
        loading: false,
        error: ""
    },
    confirmRemove: {
        isOpen: false,
        id: "",
    },
    loading: false,
    summaries: [

    ]
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

        case "OPEN_EMPLOYEE_RECORDS": return { ...state, access: { ...state.access, isOpen: true, employee: payload } };

        case "CLOSE_EMPLOYEE_RECORDS": return { ...state, access: { ...state.access, isOpen: false, employee: null } };

        case "FETCH_EMPLOYEE_RECORDS": return { ...state, access: { ...state.access, loading: true } };

        case "FETCHED_EMPLOYEE_RECORDS": return { ...state, access: { ...state.access, loading: false, records: payload } };

        case "REJECTED_EMPLOYEE_RECORDS": return { ...state, access: { ...state.access, loading: false, records: [] } };

        case "FETCH_EMPLOYEE_SUMMARIES": return { ...state, loading: true };

        case "FETCHED_EMPLOYEE_SUMMARIES": return { ...state, loading: false, summaries: payload };

        case "REJECTED_EMPLOYEE_SUMMARIES": return { ...state, loading: false, summaries: [] };

        case "SHOW_ADD_EMPLOYEE":
            return { ...state, addEmployee: { ...state.addEmployee, isOpen: true }, access: { ...state.access } };

        case "HIDE_ADD_EMPLOYEE":
            return { ...state, addEmployee: { ...state.addEmployee, loading: false, errorMessage: "", isOpen: false }, access: { ...state.access } };

        case "CREATE_EMPLOYEE_FAILED":
            return { ...state, addEmployee: { ...state.addEmployee, errorMessage: payload.Reason } }

        case "CREATE_EMPLOYEE":
            return { ...state, addEmployee: { ...state.addEmployee, loading: true, error: "" } }

        case "CREATED_EMPLOYEE":
            return { ...state, addEmployee: { ...state.addEmployee, loading: false } }

        case "SHOW_PENDING":
            return { ...state, pending: { ...state.pending, isOpen: true } };

        case "HIDE_PENDING":
            return { ...state, pending: { ...state.pending, isOpen: false } };

        case "FETCH_PENDING_ACCOUNTS":
            return { ...state, pending: { ...state.pending, loading: true } };

        case "FETCHED_PENDING_ACCOUNTS":
            return { ...state, pending: { ...state.pending, loading: false, accounts: payload } };

        case "REMOVE_PENDING_ACCOUNT": return {...state, remove: {...state.remove, loading: true}};

        case "REMOVED_PENDING_ACCOUNT": return {...state, remove: {...state.remove, loading: false}};

        case "REMOVE_PENDING_ACCOUNT_FAILED": return {...state, remove: {...state.remove, loading: false, error: payload.Reason}};

        case "SHOW_CONFIRM_REMOVE_EMPLOYEE": return {...state, confirmRemove: {...state.confirmRemove, isOpen: true, id: payload}};

        case "HIDE_CONFIRM_REMOVE_EMPLOYEE": return {...state, confirmRemove: {...state.confirmRemove, isOpen: false, id: ""}};        



        default:
            return state
    }
}
