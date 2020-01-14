import React, { Component } from 'react'
import {Button, CardContent, Card, CardHeader, TextField, Grid} from '@material-ui/core'
import {login, loginFormUpdated} from '../actions/accountActions';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';

const gridWrapperClass = {
    paddingBottom: '5%'
}

const wrapperClass = {
    padding: '1%',
    width: '50%'
}


class Login extends Component {    

    constructor(props) {
        super(props);
        console.log(this.props);                
    }
    


    componentDidMount() {
        console.log(this.props);        
    }    




    handleSubmit = (e) => {        
        e.preventDefault();            

        this.props.dispatch(login(this.props.user.username, this.props.user.password));
    }


    render() {        

        if(this.props.isLoggedIn) {
            this.props.history.push("/landing");
        }

        const body = this.props.isLoading ? <h1>Loading</h1> : 
                    <form onSubmit={this.handleSubmit}>
                        <Grid direction="column" container>
                            <Grid style={gridWrapperClass}>
                            <TextField value={this.props.user.username} required id="email" label="Email" onChange={(e) => this.props.dispatch(loginFormUpdated(e.target.value, this.props.user.password))}></TextField>
                            </Grid>                            
                            <Grid style={gridWrapperClass}>
                            <TextField value={this.props.user.password} required id="password" label="Password" type="password"  onChange={(e) => this.props.dispatch(loginFormUpdated(this.props.user.username, e.target.value))}></TextField>
                            </Grid>                            
                            <Grid style={gridWrapperClass}>
                            <Button variant="contained" type="submit">Submit</Button>
                            </Grid>                            
                        </Grid>
                    </form>
                    
                    
        return (
            <div align="center">
            <Card variant="outlined" align="center" style={wrapperClass}>
                <CardHeader title="System Login">                                     
                </CardHeader>
                <CardContent>         
                    {body}           
                </CardContent>
            </Card>
            </div>
        )
    }
}


const mapStateToProps = (state) => {
     return {
         isLoading: state.account.loading,
         error: state.account.error,
         isLoggedIn: state.account.isLoggedIn,
         user: {
             username: state.login.email,
             password: state.login.password
         }
     }
}


export default withRouter(connect(mapStateToProps)(Login));
