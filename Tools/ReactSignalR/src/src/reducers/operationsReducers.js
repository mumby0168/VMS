
const intialState = {    
    operations: []
}


export default function(state = intialState , action) {
    switch(action.type) {

        case "OPERATION_PUSHED":
            state.operations.push(action.payload);
            return state;
        default:
            return state;

    }
}