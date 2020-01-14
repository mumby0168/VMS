export function showSidebar(bool) {
    return function(dispatch){
        dispatch({type: "TOGGLE_SIDE_BAR", payload: bool})
    }
}