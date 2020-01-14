import React, { Component } from 'react'
import { List, ListItemText, Divider } from '@material-ui/core'
import ListItemLink from '../common/ListItemLink'
import { connect } from 'react-redux'
import InputIcon from '@material-ui/icons/Input';
import HomeIcon from '@material-ui/icons/Home';
import {logout} from '../actions/accountActions'

const listStyle = {
    padding: '20px',
    height: '100%'
}

const itemstyle = {
    width: 'auto'
}

const bottomButton = {
    bottom: '0px',
    width: 'auto'
}

class Sidebar extends Component {    


    render() {   
        
        var welcomeMessage = this.props.email === null ? "Please login." : <h4>Hello, {this.props.email}</h4>

        var loginOrLogout = this.props.email !== null ? <ListItemLink onClick={(e) => this.props.dispatch(logout())} style={bottomButton} className="fixed-bottom" icon={<HomeIcon/>} to="/login" primary="Logout"/> : <ListItemLink style={itemstyle} icon={<InputIcon/>} to="/login" primary="Login"/>         

        return (                                                
                <List style={listStyle} component="nav">
                    <ListItemText>
                        {welcomeMessage}
                    </ListItemText>
                    <Divider/>
                    {loginOrLogout}
                    <ListItemLink style={itemstyle} icon={<HomeIcon/>} to="/landing" primary="Landing">                    
                    </ListItemLink>                                                                       
                                                                                       
                </List>                                              
        )
    }
}


const mapStateToProps = (state) => {
    return {
        email: state.account.userDetails.email
    }
}


export default connect(mapStateToProps)(Sidebar);
