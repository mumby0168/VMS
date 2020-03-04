import {VisitorEvents} from "../events/visitorEvents";
import {IActionUnion, makeAction} from "../../redux-helpers/helpers";

export interface IVisitor {
    id: string;
    name: string;
    inAt: string;
}

export const fetchVisitors = makeAction<VisitorEvents.FETCH_VISITORS, boolean>(VisitorEvents.FETCH_VISITORS);

export const fetchedVisitors = makeAction<VisitorEvents.FETCHED_VISITORS, IVisitor[]>(VisitorEvents.FETCHED_VISITORS);

export const rejectedVisitors = makeAction<VisitorEvents.REJECTED_VISITORS, string>(VisitorEvents.REJECTED_VISITORS);


const actions = {
    fetchVisitors, fetchedVisitors, rejectedVisitors
};

export type IVisitorsAction = IActionUnion<typeof actions>;