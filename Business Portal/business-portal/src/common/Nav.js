import React, { Component } from 'react'
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import IconButton from '@material-ui/core/IconButton';
import {Drawer} from '@material-ui/core'
import MenuIcon from '@material-ui/icons/Menu';
import { connect } from 'react-redux';
import {showSidebar} from '../actions/uiActions'
import Sidebar from './Sidebar';

class Nav extends Component {

    showSidebar = (visible) => {
        this.props.dispatch(showSidebar(visible));
    }    


    render() {
        return (
            <div>
            <AppBar position="static">
                <Toolbar>
                    <IconButton edge="start" onClick={(e) => this.showSidebar(true)} color="inherit" aria-label="menu">
                        <MenuIcon ></MenuIcon>
                    </IconButton>                    
                </Toolbar>
            </AppBar>
            <Drawer open={this.props.isSidebarVisible} onClose={(e) => this.showSidebar(false)}>
                <Sidebar></Sidebar>
            </Drawer>
            </div>
        )
    }
}



export default connect((state) => {
    return {isSidebarVisible: state.ui.isSidebarVisible}
})(Nav)
