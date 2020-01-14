export default function reducer(state = { 
    pendingOperations: [],
    operationHandler: [],
    isSignalRConnected: false
}, action)
{
    switch(action.type) {

        case "OPERATION_PUSHED": {
            return {...state}
        }

        case "HANDLE_ADDED": {
            return {...state}
        }

        case "SIGNALR_CONNECTION_UPDATED": {
            return {...state, isSignalRConnected: action.payload};
        }
        
        default: return state;
    }
}