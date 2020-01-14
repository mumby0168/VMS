export function showSidebar(bool) {
    return function(dispatch){
        dispatch({type: "TOGGLE_SIDE_BAR", payload: bool})
    }
}

export function updateLandingTab(index) {
    return {type: "UPDATE_LANDING_TAB", payload: index}
}