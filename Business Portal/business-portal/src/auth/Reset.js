import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { updateResetForm, resetPasswordComplete } from '../actions/accountActions';
import Progress from '../common/Progress';
import Alert from '@material-ui/lab/Alert';
import { Card, CardContent, CardHeader,Grid, TextField, Button, Backdrop, Typography} from '@material-ui/core';

const gridWrapperClass = {
    paddingBottom: '5%'
}

class Reset extends Component {

    componentDidMount() {        
        let code = this.props.match.params.code;
        this.props.dispatch(updateResetForm({...this.props.formData, code: code}));
    }

    submit(e) {        
        e.preventDefault();        
        console.log(this.props);
        this.props.dispatch(resetPasswordComplete(this.props.formData.email, this.props.formData.password, this.props.formData.passwordConfirm, this.props.formData.code));
    }

    resetUpdated(data) {
        this.props.dispatch(updateResetForm(data));
    }    

    confirm = () => {
        this.props.dispatch({type: "RESET_USER_CONFIRMED"});
        this.props.history.push("/login");        
    }


    render() {

    const error = (this.props.errorMessage === null) ? "" : 
    <Grid style={gridWrapperClass}>
        <Alert severity="error">{this.props.errorMessage}</Alert>
    </Grid>  

    const content = this.props.loading ? <Progress message="Attempting to reset your password"/> :
    <form onSubmit={this.submit.bind(this)}>
        <Grid direction="column" container>
                <Grid style={gridWrapperClass}>
                    <TextField value={this.props.email} required id="email" label="Email" 
                    onChange={(e) => this.resetUpdated({...this.props.formData, email: e.target.value})}/>
                </Grid>
                <Grid style={gridWrapperClass}>
                    <TextField value={this.props.password} type="password" required id="password" label="Password" 
                    onChange={(e) => this.resetUpdated({...this.props.formData, password: e.target.value})}/>
                </Grid>
                <Grid style={gridWrapperClass}>
                    <TextField value={this.props.passwordConfirm} type="password" required id="passwordConfirm" label="Confirm Password" 
                    onChange={(e) => this.resetUpdated({...this.props.formData, passwordConfirm: e.target.value})}/>                    
                </Grid>
                {error}                
                <Grid style={gridWrapperClass}>
                    <Button variant="contained" color="primary" type="submit">Submit</Button>
                </Grid>       
        </Grid>
    </form>



        return (
            <div align="center">
                <Backdrop style={{zIndex: 250}} open={this.props.accepted} onClick={this.confirm}>
                    <Card>
                        <CardContent>
                            <Typography>Your password has been reset please proceed to the login.</Typography>
                            <Button variant="contained" color="primary" onClick={this.confirm}>Login</Button>
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


const mapStateToProps = (state) => {
    return {
        loading: state.account.resetLoading,
        errorMessage: state.account.error.reason,
        accepted: state.account.resetValid,
        formData: {
            email: state.account.resetRequest.email,
            password: state.account.resetRequest.password,
            passwordConfirm: state.account.resetRequest.passwordConfirm,
            code: state.account.resetRequest.code,
        }
    }
}

export default withRouter(connect(mapStateToProps)(Reset));


