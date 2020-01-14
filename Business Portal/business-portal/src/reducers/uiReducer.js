export default function reducer(state = {
    "isSidebarVisible": false
}
, action) {
    switch (action.type) {
        
        case "TOGGLE_SIDE_BAR": {
            return {...state, isSidebarVisible: action.payload};
        }

        default:
            return state;
    }
}