import { getSiteAvailability } from "./siteActions"




export function showSidebar(bool) {
    return function(dispatch){
        dispatch({type: "TOGGLE_SIDE_BAR", payload: bool})
    }
}

export function updateLandingTab(index) {
    return {type: "UPDATE_LANDING_TAB", payload: index}
}

export function updateSitesTab(index, id) {
    return (dispatch) => {
        dispatch({type: "UPDATE_SITES_TAB", payload: index})
        dispatch(getSiteAvailability(id));
    }   
    
}

export function showSiteSpinner(message) {
    return {
        type: "SHOW_SITE_SPINNER",
        payload: message
    }
}

export function hideSiteSpinner() {
    return {
        type: "HIDE_SITE_SPINNER",        
    }
}

export function updateTheme(isDark) {
    return {type: "THEME_UPDATED", payload: isDark};
}