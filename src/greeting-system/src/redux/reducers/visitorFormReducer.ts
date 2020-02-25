import { IVisitorFormActions, IVisitorDataSpecification } from "../actions/visitorFormActions";
import { VisitorFormEvents } from "../events/visitorFormEvents";



export interface IVisitorFormState {
    errors: string[];
    fields: IVisitorDataSpecification[],
    loading: boolean;
}

const initState: IVisitorFormState = {
    errors: [],
    fields: [],
    loading: false
}

export const reducer = (state: IVisitorFormState = initState, action: IVisitorFormActions): IVisitorFormState => {
    switch (action.type) {

        case VisitorFormEvents.UPDATED:
            return {
                ...state,
                fields: state.fields.map((spec, index) => {

                    if (index === action.payload.index) {
                        spec.value = action.payload.newValue;
                        return spec;
                    }

                    return spec;
                })
            }

        case VisitorFormEvents.ERRORS: return { ...state, errors: action.payload }

        case VisitorFormEvents.FETCH_SPEC:            
            return {...state, loading: action.payload};

        case VisitorFormEvents.FETCHED_SPEC:
            const fields = action.payload;           
            for(let i = 0; i < fields.length; i++) {fields[i].value = ''}
            return {...state, fields: fields};

        default: return state;
    }
}