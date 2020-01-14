import React, { Component } from 'react'
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import { connect } from 'react-redux';
import {updateLandingTab} from '../actions/uiActions';
import { TabPanel } from '../components/landing/TabPanel';

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

    render() {
        return (
            <div>
                <AppBar position="static">
                <Tabs value={this.props.value} onChange={this.handleChange} aria-label="simple tabs example">
                    <Tab label="Access Records" {...this.a11yProps(0)} />
                    <Tab label="Visitor Records" {...this.a11yProps(1)} />                    
                </Tabs>
                </AppBar>
                <TabPanel value={this.props.value} index={0}>
                    ACCESS RECORDS LIST
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
        value: state.ui.landingTabIndex
    }
}

export default connect(mapStateToProps)(Landing);
