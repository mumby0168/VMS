import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Card, CardContent, CardHeader, Button, Grid } from '@material-ui/core'
import { withRouter } from 'react-router-dom'
import TextFieldWrapper from '../../common/TextFieldWrapper'
import { completeAccount } from '../../actions/accountActions'

const formUpdate = (property, value, data) => {
    data[property] = value

    return {
        type: "COMPLETE_ACCOUNT_FORM_UPDATED",
        payload: {
            email: data.email,
            password: data.password,
            passwordConfirm: data.passwordConfirm
        }
    }
}

const codeUpdate = (code) => {
    return {
        type: "COMPLETE_ACCOUNT_CODE_UPDATED",
        payload: code
    }
}

class CompleteAccount extends Component {


    componentDidMount() {
        var code = this.props.match.params.code;
        this.props.dispatch(codeUpdate(code));
    }

    submit(e) {
        e.preventDefault();
        this.props.dispatch(completeAccount(this.props.code, this.props.formData.email, this.props.formData.password, this.props.formData.passwordConfirm))
    }


    render() {
        return (
            <div align="center">
                <Card style={{
                    padding: '1rem',
                    maxWidth: '600px'
                }}>
                    <CardHeader title="Complete your account" />
                    <CardContent>
                        <form onSubmit={this.submit.bind(this)}>
                            <div style={{ paddingRight: '1rem', paddingLeft: '1rem' }}>
                                <TextFieldWrapper
                                    margin="dense"
                                    fullWidth
                                    value={this.props.formData.email}
                                    name="email"
                                    error={false}
                                    label="Email Address"
                                    onChange={(e) => this.props.dispatch(formUpdate(e.target.name, e.target.value, this.props.formData))}
                                />
                            </div>
                            <Grid container>
                                <Grid item md={6}
                                    xs={12}>
                                    <div style={{ paddingRight: '1rem', paddingLeft: '1rem' }}>
                                        <TextFieldWrapper
                                            fullWidth
                                            margin="dense"
                                            value={this.props.formData.password}
                                            error={false}
                                            name="password"
                                            type="password"
                                            label="Password"
                                            onChange={(e) => this.props.dispatch(formUpdate(e.target.name, e.target.value, this.props.formData))}
                                        />
                                    </div>
                                </Grid>
                                <Grid md={6} xs={12} item>
                                    <div style={{ paddingRight: '1rem', paddingLeft: '1rem' }}>
                                        <TextFieldWrapper
                                            fullWidth
                                            margin="dense"
                                            type="password"
                                            error={false}
                                            value={this.props.formData.passwordConfirm}
                                            name="passwordConfirm"
                                            label="Confirm Password"
                                            onChange={(e) => this.props.dispatch(formUpdate(e.target.name, e.target.value, this.props.formData))}
                                        />
                                    </div>
                                </Grid>
                            </Grid>

                            <Button style={{
                                marginTop: '1rem',
                                marginLeft: '1rem',
                                float: 'right'
                            }} variant="contained" color="secondary" type="submit">Submit</Button>
                            <Button style={{
                                marginTop: '1rem',
                                float: 'left'
                            }} variant="contained"
                                onClick={(e) => this.props.history.push('/login')}>Login</Button>
                        </form>
                    </CardContent>
                </Card>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        formData: {
            email: state.completeAccount.email,
            password: state.completeAccount.password,
            passwordConfirm: state.completeAccount.passwordConfirm
        },
        code: state.completeAccount.code
    }
}

export default withRouter(connect(mapStateToProps)(CompleteAccount));
