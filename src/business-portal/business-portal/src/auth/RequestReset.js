import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import Alert from '@material-ui/lab/Alert';
import { connect } from 'react-redux'
import {CardContent, Card, CardHeader, Grid, TextField, Button, Backdrop, Typography} from '@material-ui/core'
import {requestPasswordReset} from '../actions/accountActions'
import ProgressBar from '../common/Progress'

const gridWrapperClass = {
    paddingBottom: '2%'
}

class RequestReset extends Component {


    requestResetUpdated = (email) => {
        this.props.dispatch({type: "RESET_REQUEST_FORM_UPDATED", payload: email});
    }

    submit = (e) => {
        e.preventDefault();
        this.props.dispatch(requestPasswordReset(this.props.email));
    }

    login = () => {
        this.props.history.push('/login');
    }    

    confirm = () => {
        this.props.dispatch({type: "REQUEST_RESET_USER_CONFIRMED"});
        this.props.history.push("/login");        
    }



    render() {

        const error = (this.props.error === null) ? "" : 
        <Grid style={gridWrapperClass}>
            <Alert severity="error">{this.props.error}</Alert>
        </Grid>  
        
        const content = this.props.loading ? <ProgressBar message="Processing request" /> : 
        <form onSubmit={this.submit}>
            <Grid direction="column" container>
                <Grid style={gridWrapperClass}>
                    <TextField value={this.props.email} required id="email" label="Email" onChange={(e) => this.requestResetUpdated(e.target.value)}></TextField>
                </Grid>
                {error}
                <Grid style={gridWrapperClass}>
                    <Button onClick={this.login}>Back to login</Button>
                </Grid>
                <Grid style={gridWrapperClass}>
                    <Button variant="contained" color="primary" type="submit">Submit</Button>
                </Grid>
            </Grid>
        </form>
        
        
        
        return (
            <div align="center">
                <Backdrop style={{zIndex: 250}} open={this.props.shouldConfirm} onClick={this.confirm}>
                    <Card>
                        <CardContent>
                            <Typography>You will shortly receive an email with instructions to reset your password.</Typography>
                            <Button variant="contained" color="primary" onClick={this.confirm}>Close</Button>
                        </CardContent>
                    </Card>
                </Backdrop>
                <Card style={{maxWidth: '50%'}}>
                    <CardHeader title="Reset Passsword"></CardHeader>
                    <CardContent>
                        {content}
                    </CardContent>
                </Card>
            </div>
        )
    }
}

const mapStateToProps = ((state) => {
    return {
        loading: state.account.resetRequestLoading,        
        error: state.account.error.reason,
        email: state.requestReset.email,
        shouldConfirm: state.requestReset.requestSent,
    }
})

export default withRouter(connect(mapStateToProps)(RequestReset));
