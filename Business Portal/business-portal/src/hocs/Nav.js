import React, { Component } from 'react'
import AppBar from '@material-ui/core/AppBar';
// import IconButton from '@material-ui/core/IconButton';
// import MenuIcon from '@material-ui/icons/Menu';
import {Drawer} from '@material-ui/core'
import { connect } from 'react-redux';
import {showSidebar} from '../actions/uiActions'
import {logout} from '../actions/accountActions'
import Sidebar from '../common/Sidebar';
import Menu from '../components/nav/Menu';
import { withRouter } from 'react-router-dom';

class Nav extends Component {

    showSidebar = (visible) => {
        this.props.dispatch(showSidebar(visible));
    }    

    logoutHandle  = () => {        
        this.props.dispatch(logout());        
    }


    render() {
        return (
            <div>
            <AppBar position="static">                
                    {/* <IconButton edge="start" onClick={(e) => this.showSidebar(true)} color="inherit" aria-label="menu">
                        <MenuIcon ></MenuIcon>
                    </IconButton>                     */}
                    <Menu role={this.props.role} logout={this.logoutHandle}></Menu>                
            </AppBar>
            <Drawer open={this.props.isSidebarVisible} onClose={(e) => this.showSidebar(false)}>
                <Sidebar></Sidebar>
            </Drawer>
            </div>
        )
    }
}



export default withRouter(connect((state) => {
    return {
        isSidebarVisible: state.ui.isSidebarVisible,
        role: state.account.userDetails.role
    }
})(Nav));
