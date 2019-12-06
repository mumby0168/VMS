export function addOperation(operation) {
    return {
        type: "OPERATION_PUSHED",
        payload: operation
    };
}