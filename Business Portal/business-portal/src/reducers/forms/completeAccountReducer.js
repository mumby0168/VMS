const initialState = {
    code: "",
    email: "",
    password: "",
    passwordConfirm: "",
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

    case "COMPLETE_ACCOUNT_FORM_UPDATED":
        return { ...state, email: payload.email, password: payload.password, passwordConfirm: payload.passwordConfirm};
        
    case "COMPLETE_ACCOUNT_CODE_UPDATED":
        return {...state, code: payload};

    default:
        return state
    }
}
