import React, { Component } from 'react'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { Tabs, Tab, Paper } from '@material-ui/core'
import {updateSitesTab} from '../actions/uiActions'
import { PanelsList } from '../components/availability/PanelsList'


class Availability extends Component {    

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

    render() {

        const tabs = this.props.siteSummaries.map((summary, index) => {
            return <Tab key={index} label={summary.name} {...this.a11yProps(index)}></Tab>        
        });

        return (
        <Paper style={{
            display: 'flex',
            flexGrow: 1,
        }}>
            <Tabs
            orientation="vertical"
            variant="scrollable"                
            aria-label="Vertical tabs example"  
            onChange={this.handleChange}
            value={this.props.value}>                
                {tabs}
            </Tabs>     

            <PanelsList availability={this.props.activeAvailability} siteSummaries={this.props.siteSummaries} value={this.props.value} ></PanelsList> 
 
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
