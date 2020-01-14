import React, { Component } from 'react'
import { List } from '@material-ui/core'
import ListItemLink from '../common/ListItemLink'
import { connect } from 'react-redux'
import InputIcon from '@material-ui/icons/Input';
import HomeIcon from '@material-ui/icons/Home';

const listStyle = {
    width: '400px'
}

const itemstyle = {
    width: 'auto'
}

class Sidebar extends Component {    


    render() {   
        
        var welcomeMessage = this.props.email === null ? "" : <h2>Hello, {this.props.email}</h2>

        return (            
            <div>
                {welcomeMessage}
                <List style={listStyle} component="nav">
                    <ListItemLink style={itemstyle} icon={<InputIcon/>} to="/login" primary="Login">                    
                    </ListItemLink>                
                    <ListItemLink style={itemstyle} icon={<HomeIcon/>} to="/landing" primary="Landing">                    
                    </ListItemLink>                                                                       
                </List>                            
            </div>                

        )
    }
}


const mapStateToProps = (state) => {
    return {
        email: state.account.userDetails.email
    }
}


export default connect(mapStateToProps)(Sidebar);
