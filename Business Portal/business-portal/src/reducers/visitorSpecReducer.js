

const initialState = {
    loadingSpecs: false,
    specifications: [],
    deprecated: {
        loading: false,
        specifications: [],
        open: false,
    },
    add: {
        open: false,
        options: [
            "Required"
        ],
        form: {
            label: "",
            code: "",
            message: ""
        }
    }
}



export default (state = initialState, { type, payload }) => {
    switch (type) {

        case "CLEAR_ADD_SPEC": {
            return {...state, add: {...state.add, form: initialState.add.form}};
        }

        case "OPEN_ADD_SPEC":
            return { ...state, add: { ...state.add, open: true } };

        case "OPEN_DEPRECTATED_SPECS":
            return { ...state, add: { ...state.deprecated, open: true } };

        case "CLOSE_ADD_SPEC":
            return { ...state, add: { ...state.add, open: false } }

        case "CLOSE_DEPRECTATED_SPECS":
            return { ...state, deprecated: { ...state.deprecated, open: false } }

        case "FETCH_SPECS":
            return { ...state, loadingSpecs: true }

        case "FETCHED_SPECS":
            return { ...state, loadingSpecs: false, specifications: payload };

        case "UPDATE_ADD_SPEC_FORM": 
            return {...state, add: {
                ...state.add,
                form: {
                    ...state.add.form,
                    [payload.key]: payload.value
                }
            }}

        default:
            return state
    }
}
