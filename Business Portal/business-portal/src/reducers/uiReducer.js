export default function reducer(state = {
    isSidebarVisible: false,
    landingTabIndex: 0,
    sitesTabIndex: 2,
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

        default:
            return state;
    }
}