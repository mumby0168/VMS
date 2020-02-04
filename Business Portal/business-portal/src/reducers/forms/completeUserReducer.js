const initialState = {
    data: {
        accountId: "",
        basedSiteId: "",
        firstName: "",
        secondName: "",
        phone: "",
        businessPhone: "",
        selectedSiteId: ""
    },  
    errors: {
        firstName: false,
        secondName: false,
        phone: false,
        businessPhone: false,
    },
    goToDashboard: false
}

export default (state = initialState, { type, payload }) => {
    switch (type) {

        case "COMPLETE_USER_FORM_UPDATED": return {
            ...state,
            data: { 
                ...state.data,               
                accountId: payload.accountId,
                basedSiteId: payload.basedSiteId,
                firstName: payload.firstName,
                secondName: payload.secondName,
                phone: payload.phone,
                businessPhone: payload.businessPhone,
            }            
        }

        case "COMPLETE_USER_BLUR_UPDATED": {
            return {...state, errors: {...state.errors, [payload]: true}};
        }

        case "COMPLETE_USER_SITE_UPDATED": {
            return {...state, data: {...state.data, selectedSiteId: payload}}
        }

        case "USER_CREATION_COMPLETE": return {...state, goToDashboard: true};

        default:
            return state
    }
}
