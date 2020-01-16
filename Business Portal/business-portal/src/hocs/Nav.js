import React, { Component } from 'react'
import AppBar from '@material-ui/core/AppBar';
import { connect } from 'react-redux';
import {showSidebar} from '../actions/uiActions'
import {logout} from '../actions/accountActions'
import Menu from '../components/nav/Menu';
import { withRouter } from 'react-router-dom';
import { teal } from '@material-ui/core/colors';

const appBarColor = teal[700];

const appBarStyle = {
    backgroundColor: appBarColor
}

class Nav extends Component {

    showSidebar = (visible) => {
        this.props.dispatch(showSidebar(visible));
    }    

    logoutHandle  = () => {        
        this.props.dispatch(logout());        
    }


    render() {        
        return (            
            <AppBar style={appBarStyle} position="static">                                   
                <Menu initials={this.props.initials} role={this.props.role} businessName={this.props.businessName} logout={this.logoutHandle}></Menu>                
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
