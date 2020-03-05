import {VisitorEvents} from "../events/visitorEvents";
import {IActionUnion, makeAction} from "../../redux-helpers/helpers";

export interface IVisitor {
    id: string;
    name: string;
    inAt: string;
}

export interface ISelectedVisitorUpdate {
    id: string;
    name: string;
}

export const fetchVisitors = makeAction<VisitorEvents.FETCH_VISITORS, boolean>(VisitorEvents.FETCH_VISITORS);

export const fetchedVisitors = makeAction<VisitorEvents.FETCHED_VISITORS, IVisitor[]>(VisitorEvents.FETCHED_VISITORS);

export const rejectedVisitors = makeAction<VisitorEvents.REJECTED_VISITORS, string>(VisitorEvents.REJECTED_VISITORS);

export const updateSelectedVisitor = makeAction<VisitorEvents.VISITOR_SELECTED, ISelectedVisitorUpdate>(VisitorEvents.VISITOR_SELECTED);

export const visitorSelectionConfirmed = makeAction<VisitorEvents.VISITOR_SELECTION_CONFIRMED, boolean>(VisitorEvents.VISITOR_SELECTION_CONFIRMED);

export const visitorSelectionCancelled = makeAction<VisitorEvents.VISITOR_SELECTION_CANCELLED, boolean>(VisitorEvents.VISITOR_SELECTION_CANCELLED);


const actions = {
    fetchVisitors, fetchedVisitors, rejectedVisitors, updateSelectedVisitor, visitorSelectionConfirmed, visitorSelectionCancelled
};

export type IVisitorsAction = IActionUnion<typeof actions>;