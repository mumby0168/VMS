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

const Home = () => {
    return <h1>Home</h1>
}


class Layout extends Component {
    

    render() {

        console.log("user is logged in? " + this.props.valid);

        return (
            <React.Fragment>
                <CssBaseline/>
                <Operations></Operations>
                <Router>
                <Nav></Nav>      
                <div className="content-wrapper">
                <Container className="content-wrapper" >                                                    
                    <Switch>
                        <Route exact path="/">
                            <Home></Home>
                        </Route>
                        <Route path="/login">
                            <Login></Login>
                        </Route>
                        <Route path="/password-reset">
                            <RequestReset></RequestReset>
                        </Route>
                        <Route path="/reset/:code">
                            <Reset/>
                        </Route>
                        <PrivateRoute path="/firelist" valid={this.props.valid}>
                            <Fire/>
                        </PrivateRoute>
                        <PrivateRoute path="/landing" valid={this.props.valid}>
                            <Landing></Landing>
                        </PrivateRoute>
                        <PrivateRoute path="/availability" valid={this.props.valid}>
                            <Availability></Availability>
                        </PrivateRoute>
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


const mapStateToProps = (state) => {return {valid: state.account.isLoggedIn} } 

export default connect(mapStateToProps)(Layout);



