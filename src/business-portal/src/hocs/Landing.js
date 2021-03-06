import React, { Component } from 'react'
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import { connect } from 'react-redux';
import { updateLandingTab } from '../actions/uiActions';
import { TabPanel } from '../components/landing/TabPanel';
import AccessRecordsList from '../components/landing/AccessRecordsList';
import { getPersonalAccessRecords } from '../actions/accessRecordActions'
import Progress from '../common/Progress';
import { Typography, Grid } from '@material-ui/core';
import { getBusinessInfo } from '../actions/businessActions'
import { getSiteSummaries } from '../actions/siteActions';
import InOut from '../components/landing/InOut';
import { withRouter } from 'react-router-dom';


class Landing extends Component {


    a11yProps = (index) => {
        return {
            id: `simple-tab-${index}`,
            'aria-controls': `simple-tabpanel-${index}`,
        };
    }

    handleChange = (event, newValue) => {
        this.props.dispatch(updateLandingTab(newValue));
    };

    componentDidMount() {        
        this.props.dispatch(getSiteSummaries());
        this.props.dispatch(getPersonalAccessRecords())
        this.props.dispatch(getBusinessInfo(this.props.businesId));
    }    

    render() {

        console.log("Checking for user data.");
        console.log(this.props.userDataAvailable);

        if(this.props.userDataAvailable === false) {
            this.props.history.push(`/create/${this.props.accountId}`);
        }

        return (
            <div style={{ height: '100%' }}>
                <Grid style={{marginBottom: '5px'}} container>
                    <Grid item md={8}>
                        <Typography style={{ paddingBottom: '3px' }} variant="h5">Good {new Date().getHours() > 12 ? "Afternoon" : "Morning"}, {this.props.name}</Typography>
                    </Grid>

                    <Grid item md={4}>
                        <InOut siteId={this.props.siteId} userId={this.props.userId} userCode={this.props.userCode} dispatchHandle={this.props.dispatch} />
                    </Grid>

                </Grid>

                <AppBar position="static">
                    <Tabs value={this.props.value} onChange={this.handleChange} aria-label="simple tabs example">
                        <Tab label="Access Records" {...this.a11yProps(0)} />
                        <Tab label="Visitor Records" {...this.a11yProps(1)} />
                    </Tabs>
                </AppBar>
                <TabPanel value={this.props.value} index={0}>
                    {this.props.accessRecordsLoading ? <Progress message="Loading access records" /> : <AccessRecordsList records={this.props.accessRecords}></AccessRecordsList>}
                </TabPanel>
                <TabPanel value={this.props.value} index={1}>
                    VISITOR RECORDS LIST
                </TabPanel>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        value: state.ui.landingTabIndex,
        accessRecords: state.access.records,
        accessRecordsLoading: state.access.loading,
        businesId: state.account.userDetails.businessId,
        name: state.user.firstName + " " + state.user.secondName,
        userId: state.user.id,
        userCode: state.user.code,
        siteId: state.user.basedSiteId,
        userDataAvailable: state.user.isAvailable,
        accountId: state.account.userDetails.id,
    }
}

export default withRouter(connect(mapStateToProps)(Landing));