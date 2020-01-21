import React, { Component } from 'react'
import AppBar from '@material-ui/core/AppBar';
import { connect } from 'react-redux';
import {showSidebar, updateTheme} from '../actions/uiActions'
import {logout} from '../actions/accountActions'
import Menu from '../components/nav/Menu';
import { withRouter } from 'react-router-dom';

class Nav extends Component {

    showSidebar = (visible) => {
        this.props.dispatch(showSidebar(visible));
    }    

    logoutHandle  = () => {        
        this.props.dispatch(logout());        
    }

    navigate = (path) => {
        this.props.history.push(path);
    }

    handleThemeChange(isDark) {
        this.props.dispatch(updateTheme(isDark));
    }


    render() {        
        return (            
            <AppBar position="static">                                   
                <Menu handleThemeChange={this.handleThemeChange.bind(this)} navigate={this.navigate} initials={this.props.initials} role={this.props.role} businessName={this.props.businessName} logout={this.logoutHandle}></Menu>                
            </AppBar>                        
        )
    }
}



export default withRouter(connect((state) => {
    return {
        isSidebarVisible: state.ui.isSidebarVisible,
        role: state.account.userDetails.role,
        businessName: state.business.name,
        initials: state.user.initials,
    }
})(Nav));
