import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { Paper, Grid, createMuiTheme } from '@material-ui/core'
import {updateSitesTab} from '../actions/uiActions'
import { PanelsList } from '../components/availability/PanelsList'
import { getSiteSummaries } from '../actions/siteActions'
import AvailabilityTabs from '../components/availability/AvailaiblityTabs'
import SizeAwareGridContainer from '../common/SizeAwareGridContainer'


class Availability extends Component {    


    componentDidMount()
    {
        this.props.dispatch(getSiteSummaries());
        this.handleChange(null, 0);
    }

    handleChange = (event, newValue) => {
        var current = this.props.siteSummaries[newValue];
        this.props.dispatch(updateSitesTab(newValue, current.id));
    };
    

    theme = createMuiTheme();

    render() {    

        return (
        <Paper style={{height: '100%'}}>
            <SizeAwareGridContainer>
                <Grid item xs={4} md={3}>
                    <AvailabilityTabs siteSummaries={this.props.siteSummaries} value={this.props.value} handleChange={this.handleChange.bind(this)}/>
                </Grid>
                <Grid item xs={8} md={9}>
                    <PanelsList  availability={this.props.activeAvailability} siteSummaries={this.props.siteSummaries} value={this.props.value} ></PanelsList>     
                </Grid>        
            </SizeAwareGridContainer>
        </Paper>
        )
    }
}

const mapStateToProps = ((state) => {
    return { 
        siteSummaries: state.site.summaries,
        value: state.ui.sitesTabIndex,
        activeAvailability: state.site.availaiblity,
    }
})

export default withRouter(connect(mapStateToProps)(Availability))
