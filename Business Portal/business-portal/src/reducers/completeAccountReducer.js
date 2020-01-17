const initialState = {

}

export default (state = initialState, { type, payload }) => {
    switch (type) {

    case "": return {...state};   

    default:
        return state
    }
}
