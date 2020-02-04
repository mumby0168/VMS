import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Grid } from '@material-ui/core'
import VisitorsHeader from '../components/visitors/VisitorsHeader'
import { withRouter } from 'react-router-dom'


class Visitors extends Component {


    navigateDataSpecification() {
        console.log('clicked');
        this.props.history.push('/specification')
    }




    render() {
        return (
            <Grid direction="row" container>
                <VisitorsHeader specHandle={this.navigateDataSpecification.bind(this)} />
            </Grid>
        )
    }
}


export default withRouter(connect()(Visitors))
