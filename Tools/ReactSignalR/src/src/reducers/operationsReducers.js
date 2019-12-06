
const intialState = {    
    operations: [        
    ]
}


export default function(state = intialState , action) {

    console.log(action);
    switch(action.type) {        

        case "OPERATION_PUSHED": {            
            return {...state, operations: state.operations.push(action.payload)};
        }
        default: 
            return state;

    }
}