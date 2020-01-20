const initialState = {
    messages: [

    ]
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

    case "SHOW_TOAST":        
        return { ...state, messages: [...state.messages, payload]}

    default:
        return state
    }
}
