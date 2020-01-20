import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { Paper, Grid, createMuiTheme } from '@material-ui/core'
import {updateSitesTab} from '../actions/uiActions'
import { PanelsList } from '../components/availability/PanelsList'
import { getSiteSummaries } from '../actions/siteActions'
import AvailabilityTabs from '../components/availability/AvailaiblityTabs'
// import AvailabilityOptions from '../components/availability/AvailabilityOptions'


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
        <Paper>
            <Grid container direction="row">
                <Grid md={12} item>
                    <Grid container direction="row">
                        <Grid item md={10} >
                            <AvailabilityTabs siteSummaries={this.props.siteSummaries} value={this.props.value} handleChange={this.handleChange.bind(this)}/>
                        </Grid>           
                        <Grid item md={2}>
                            {/* <AvailabilityOptions/> */}
                        </Grid>                        
                    </Grid>                    
                </Grid>
                <Grid item xs={12} md={12}>
                    <PanelsList  availability={this.props.activeAvailability} loading={this.props.loading} siteSummaries={this.props.siteSummaries} value={this.props.value} ></PanelsList>     
                </Grid>        
            </Grid>
        </Paper>
        )
    }
}

const mapStateToProps = ((state) => {
    return { 
        siteSummaries: state.site.summaries,
        value: state.ui.sitesTabIndex,
        activeAvailability: state.site.availaiblity,
        loading: state.site.loading
    }
})

export default withRouter(connect(mapStateToProps)(Availability))
