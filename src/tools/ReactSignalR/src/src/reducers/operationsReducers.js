const intialState = {
    operationsList: [
    ]
}


export default function(state = intialState , action) {

    
    
    switch(action.type) {        

        case "OPERATION_PUSHED": {                        
            return {...state, operationsList: [...state.operationsList, action.payload]};
        }
        default: 
            return state;

    }
}