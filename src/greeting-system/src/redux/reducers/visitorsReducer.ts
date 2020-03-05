import {IVisitor, IVisitorsAction} from "../actions/visitorActions";
import {act} from "react-dom/test-utils";
import {VisitorEvents} from "../events/visitorEvents";


export interface IVisitorsState {
    loading: boolean;
    visitors: IVisitor[],
    error: string;
    selectedName: string;
    selectedId: string;
    dialog: boolean;
}

const initState: IVisitorsState = {
    loading: false,
    visitors: [],
    error: "",
    selectedId: "",
    selectedName: "",
    dialog: false
}

export const reducer = (state: IVisitorsState = initState, action: IVisitorsAction) => {
    switch (action.type) {

        case VisitorEvents.FETCH_VISITORS:
            return {...state, loading: action.payload}
        case VisitorEvents.FETCHED_VISITORS:
            return {...state, visitors: action.payload, loading: false};
        case VisitorEvents.REJECTED_VISITORS:
            return {...state, loading: false, error: action.payload, visitors: []}
        case VisitorEvents.VISITOR_SELECTED:
            return {...state, selectedId: action.payload.id, selectedName: action.payload.name, dialog: true};
        case VisitorEvents.VISITOR_SELECTION_CONFIRMED:
            return {...state, dialog: false, selectedName: "", selectedId: ""};
        case VisitorEvents.VISITOR_SELECTION_CANCELLED:
            return {...state, dialog: false, selectedName: "", selectedId: ""};

        default: return state;
    }
}