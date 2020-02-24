import { makeAction, IActionUnion } from "../../redux-helpers/helpers";
import { VisitorFormEvents } from "../events/visitorFormEvents";

export interface IVisitorDataSpecification {
    id: string;
    label: string;
    validationCode: string;
    validationMessage: string;
    order: number;
    value: string;
}

export interface IDataSpecificationUpdateParams {
    index: number;
    newValue: string;
}

export const fetchVisitorFormSpecAction = makeAction<VisitorFormEvents.FETCH_SPEC, boolean>(VisitorFormEvents.FETCH_SPEC);

export const fetchedVisitorFormSpecAction = makeAction<VisitorFormEvents.FETCHED_SPEC, IVisitorDataSpecification[]>(VisitorFormEvents.FETCHED_SPEC);

export const rejectedVisitorFormSpecAction = makeAction<VisitorFormEvents.REJECTED_SPEC, string>(VisitorFormEvents.REJECTED_SPEC);

export const visitorFormUpdatedAction = makeAction<VisitorFormEvents.UPDATED, IDataSpecificationUpdateParams>(VisitorFormEvents.UPDATED);


const actions = {
    fetchVisitorFormSpecAction,
    fetchedVisitorFormSpecAction,
    rejectedVisitorFormSpecAction,
    visitorFormUpdatedAction
}

export type IVisitorFormActions = IActionUnion<typeof actions>;