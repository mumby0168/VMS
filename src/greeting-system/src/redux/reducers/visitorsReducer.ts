import {IVisitor, IVisitorsAction} from "../actions/visitorActions";
import {act} from "react-dom/test-utils";
import {VisitorEvents} from "../events/visitorEvents";


export interface IVisitorsState {
    loading: boolean;
    visitors: IVisitor[],
    error: string;
}

const initState: IVisitorsState = {
    loading: false,
    visitors: [],
    error: ""
};

export const reducer = (state: IVisitorsState = initState, action: IVisitorsAction) => {
    switch (action.type) {

        case VisitorEvents.FETCH_VISITORS:
            return {...state, loading: action.payload}
        case VisitorEvents.FETCHED_VISITORS:
            return {...state, visitors: action.payload, loading: false};
        case VisitorEvents.REJECTED_VISITORS:
            return {...state, loading: false, error: action.payload, visitors: []}


        default: return state;
    }
}