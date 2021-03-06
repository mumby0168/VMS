import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { Card, CardContent, Button } from '@material-ui/core';
import CompleteUserForm from '../components/user/CompleteUserForm';
import { getSiteSummaries } from '../actions/siteActions';
import { createUser } from '../actions/userActions';

const updateForm = (data) => {
    return {
        type: "COMPLETE_USER_FORM_UPDATED",
        payload: data
    }
}

const updateBlur = (key) => {
    return {
        type: "COMPLETE_USER_BLUR_UPDATED",
        payload: key
    }
}

const updateSelectedSite = (value) => {
    return {
        type: "COMPLETE_USER_SITE_UPDATED",
        payload: value
    }
}



class CompleteUser extends Component {


    componentDidMount() {
        var accountId = this.props.match.params.accountId;
        this.props.dispatch(getSiteSummaries());
        console.log(accountId);
    }

    handleFormSubmit() {
        this.props.dispatch(createUser(this.props.accountId, this.props.businessId, 
            this.props.data.selectedSiteId, this.props.data.firstName, 
            this.props.data.secondName, this.props.data.phone,
            this.props.data.businessPhone));
    }

    handleChange(value, key) {
        var data = this.props.data;
        data[key] = value;
        this.props.dispatch(updateForm(data));
    }

    handleSiteChanged(value) {
        this.props.dispatch(updateSelectedSite(value));
    }

    handleBlur(key) {
        var errors = this.props.errors;
        if (errors[key]) return;
        errors[key] = true;
        this.props.dispatch(updateBlur(key));
    }



    render() {

        const content = this.props.goToDashboard ? <Button onClick={(e) => this.props.history.push('/landing')}>To Dashboard</Button> : 
        <CompleteUserForm 
                        handleSiteChanged={this.handleSiteChanged.bind(this)} 
                        sites={this.props.sites} handleBlur={this.handleBlur.bind(this)} 
                        data={this.props.data} errors={this.props.errors} 
                        handleFormSubmit={this.handleFormSubmit.bind(this)}
                        handleChange={this.handleChange.bind(this)} />
        
        return (
            <div align="center">
                <Card style={{ maxWidth: '400px' }}>
                    <CardContent>
                        {content}
                    </CardContent>
                </Card>
            </div>
        )
    }
}


const mapStateToProps = (state => {
    return {
        data: state.completeUser.data,
        errors: state.completeUser.errors,
        sites: state.site.summaries,
        accountId: state.account.userDetails.id,
        businessId: state.account.userDetails.businessId,
        goToDashboard: state.completeUser.goToDashboard
    }
})

export default withRouter(connect(mapStateToProps)(CompleteUser))
