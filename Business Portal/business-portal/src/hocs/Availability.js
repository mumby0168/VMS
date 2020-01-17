import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { Tabs, Tab, Paper, Grid, createMuiTheme } from '@material-ui/core'
import {updateSitesTab} from '../actions/uiActions'
import { PanelsList } from '../components/availability/PanelsList'
import { getSiteSummaries } from '../actions/siteActions'


class Availability extends Component {    


    componentDidMount()
    {
        this.props.dispatch(getSiteSummaries());
        this.handleChange(null, 0);
    }

    a11yProps(index) {
        return {
          id: `vertical-tab-${index}`,
          'aria-controls': `vertical-tabpanel-${index}`,
        };
      }

      handleChange = (event, newValue) => {
        var current = this.props.siteSummaries[newValue];
        this.props.dispatch(updateSitesTab(newValue, current.id));
    };

    theme = createMuiTheme();

    render() {

        const tabs = this.props.siteSummaries.map((summary, index) => {
            return <Tab key={index} label={summary.name} {...this.a11yProps(index)}></Tab>        
        });

        return (
        <Paper style={{height: '100%'}}>
            <Grid container>
                <Grid item xs={2} md={3}>
                    <Tabs
                    style={{padding: this.theme.spacing(2)}}
                    orientation="vertical"
                    variant="scrollable"                
                    aria-label="Vertical tabs example"  
                    onChange={this.handleChange}
                    value={this.props.value}>                
                        {tabs}
                    </Tabs>     
                </Grid>
                <Grid item xs={10} md={9}>
                    <PanelsList  availability={this.props.activeAvailability} siteSummaries={this.props.siteSummaries} value={this.props.value} ></PanelsList>     
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
    }
})

export default withRouter(connect(mapStateToProps)(Availability))
