export default function reducer(state = {
    isSidebarVisible: false,
    landingTabIndex: 0,
    sitesTabIndex: 0,
    siteSpinner: false,
    message: null,
    isDark: true,    
    critical: {
        visible: false,
        message: null
    }
}
, action) {
    switch (action.type) {
        
        case "TOGGLE_SIDE_BAR": {
            return {...state, isSidebarVisible: action.payload};
        }

        case "UPDATE_LANDING_TAB": {
            return {...state, landingTabIndex: action.payload};
        }

        case "UPDATE_SITES_TAB": {
            return {...state, sitesTabIndex: action.payload};
        }

        case "SHOW_SITE_SPINNER": {
            return {...state, siteSpinner: true, message: action.payload}
        }

        case "HIDE_SITE_SPINNER": {
            return {...state, siteSpinner: false, message: null}
        }

        case "THEME_UPDATED": {
            return {...state, isDark: action.payload};
        }

        case "SHOW_CRITICAL_ERROR": {
            return {...state, critical: {visible: true, message: action.payload}};
        }

        case "HIDE_CRITICAL_ERROR": {
            return {...state, critical: {visible: false, message: null}}
        }

        default:
            return state;
    }
}