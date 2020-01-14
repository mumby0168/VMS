export function addOperation(operation) {
    return {
        type: "OPERATION_PUSHED",
        payload: operation
    };
}

export function addOperationHandler(id, func) {
    return {
        type: "HANDLE_ADDED",
        payload: {
            id: id,
            action: func
        }
    }
}

export function signalRConnectionUpdate(state) {
    return {
        type: "SIGNALR_CONNECTION_UPDATED",
        payload: state
    }
}