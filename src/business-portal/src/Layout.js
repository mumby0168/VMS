import React, { Component } from 'react'
import Nav from './hocs/Nav'
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom'
import CssBaseline from '@material-ui/core/CssBaseline';
import Login from './auth/Login'
import PrivateRoute from './auth/PrivateRoute';
import Landing from './hocs/Landing';
import { connect } from 'react-redux';
import Container from '@material-ui/core/Container';
import Operations from './operations/Operations';
import RequestReset from './auth/RequestReset';
import Availability from './hocs/Availability';
import Reset from './auth/Reset';
import Fire from './hocs/Fire';
import Toast from './operations/Toast';
import Visitors from './hocs/Visitors';
import Employees from './hocs/Employees';
import SiteSpinner from './common/SiteSpinner';
import CriticalError from './common/CriticalError';
import CompleteUser from './hocs/CompleteUser';
import CompleteAccount from './components/auth/CompleteAccount';
import VisitorSpec from './hocs/VisitorSpec';




class Layout extends Component {
    

    render() {

        const isAdmin = this.props.role === "BusinessAdmin";        
        console.log("valid: " + this.props.valid);
        console.log("role: " + this.props.role);

        return (
            <React.Fragment>
                <CssBaseline/>
                <Operations/>
                <Toast/>        
                <SiteSpinner/>                      
                <Router>
                <Nav></Nav>      
                <div className="content-wrapper">
                <Container className="content-wrapper" >                                                    
                    <CriticalError/>  
                    <Switch>                        
                        <Route path="/login">
                            <Login></Login>
                        </Route>
                        <Route path="/password-reset">
                            <RequestReset></RequestReset>
                        </Route>
                        <Route path="/reset/:code">
                            <Reset/>
                        </Route>
                        <Route path="/complete/:code">
                            <CompleteAccount/>
                        </Route>
                        <PrivateRoute path="/create/:accountId" valid={this.props.valid}>
                            <CompleteUser/>
                        </PrivateRoute>                    
                        <PrivateRoute path="/firelist" valid={this.props.valid}>
                            <Fire/>
                        </PrivateRoute>
                        <PrivateRoute path="/specification" valid={isAdmin}>
                            <VisitorSpec/>
                        </PrivateRoute>
                        <PrivateRoute path="/landing" valid={this.props.valid}>
                            <Landing></Landing>
                        </PrivateRoute>
                        <PrivateRoute path="/availability" valid={this.props.valid}>
                            <Availability></Availability>
                        </PrivateRoute>
                        <PrivateRoute path="/employees" valid={isAdmin}>
                            <Employees/>
                        </PrivateRoute>
                        <PrivateRoute path="/visitors" valid={isAdmin}>
                            <Visitors/>
                        </PrivateRoute>
                        <Route path='/' exact>
                            <Login/>
                        </Route>
                        <Route path="*">
                            <h1>Not Found</h1>
                        </Route>
                    </Switch>                                          
                </Container>
                </div>
            </Router>    
            </React.Fragment>                   
        )
    }
}


const mapStateToProps = (state) => {return {valid: state.account.isLoggedIn, role: state.account.userDetails.role} } 

export default connect(mapStateToProps)(Layout);



