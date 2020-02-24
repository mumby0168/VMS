import { IVisitorFormActions, IVisitorDataSpecification } from "../actions/visitorFormActions";
import { VisitorFormEvents } from "../events/visitorFormEvents";



export interface IVisitorFormState {
    errors: string[];
    fields: IVisitorDataSpecification[],
    loading: boolean;
}

const initState: IVisitorFormState = {
    errors: [],
    fields: [
        {
            id: '1',
            label: 'First Name',
            validationCode: "Required",
            validationMessage: "First name is required.",
            order: 1,
            value: ''
        },
        {
            id: '2',
            label: 'Second Name',
            validationCode: "Required",
            validationMessage: "Second name is required.",
            order: 1,
            value: ''
        }
    ],
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

        default: return state;
    }
}