const initialState = {
    isOpen: false,
    message: null
}

export default (state = initialState, { type, payload }) => {

    console.log(type);

    switch (type) {

    case "SHOW_TOAST":        
        return { ...state, message: payload, isOpen: true}

    case "REMOVE_TOAST": 
        return {...state, message: null, isOpen: false}

    default:
        return state
    }
}

