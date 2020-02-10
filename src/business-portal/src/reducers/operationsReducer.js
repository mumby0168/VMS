export default function reducer(state = {     
    pendingOperations: [],
    operationHandlers: [],
    isSignalRConnected: false
}, action)
{
    switch(action.type) {

        case "OPERATION_PUSHED": {
            return {...state, pendingOperations: [...state.pendingOperations, action.payload]}
        }

        case "HANDLE_ADDED": {
            return {...state, operationHandlers: [...state.operationHandlers, action.payload] }
        }

        case "HANDLE_REMOVED": {
            var index = state.operationHandlers.indexOf(action.payload);
            state.operationHandlers.splice(index, 1);
            return {...state }
        }

        case "SIGNALR_CONNECTION_UPDATED": {
            return {...state, isSignalRConnected: action.payload};
        }
        
        default: return state;
    }
}