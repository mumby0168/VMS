import React, { Component } from 'react'
import {CardContent, Card, CardHeader, Grid} from '@material-ui/core'
import {login} from '../actions/accountActions';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import Progress from '../common/Progress';
import LoginForm from '../components/auth/LoginForm';



const wrapperClass = {
    padding: '1%',
    height: '100%'    
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

        //TODO: Causing an error.
        if(this.props.isLoggedIn) {
            this.props.history.push("/landing");
        }

    

        const body = this.props.isLoading ? <Progress message="Logging you in"/> :         
                    <LoginForm dispatch={this.props.dispatch} handleSubmit={this.handleSubmit} user={this.props.user} error={this.props.error} />
                    
                    
        return (
            <Grid container spacing={3}>
                <Grid item xs={12} md={6}>
                    <Card variant="outlined" align="center" style={wrapperClass}>
                        <CardHeader title="System Login">                                     
                        </CardHeader>
                        <CardContent>         
                            {body}           
                        </CardContent>
                    </Card>
                </Grid>
                <Grid item xs={12} md={6}>
                    <Card align="center" style={wrapperClass} variant="outlined">
                        <CardHeader title="Information Area"></CardHeader>
                    </Card>
                </Grid>
            </Grid>
        )
    }
}


const mapStateToProps = (state) => {
     return {
         isLoading: state.account.loading,
         error: state.account.error.reason,
         isLoggedIn: state.account.isLoggedIn,
         user: {
             username: state.login.email,
             password: state.login.password
         }
     }
}


export default withRouter(connect(mapStateToProps)(Login));
