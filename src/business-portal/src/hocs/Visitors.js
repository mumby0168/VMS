import React, { Component } from 'react'
import { connect } from 'react-redux'
import { Grid } from '@material-ui/core'
import VisitorsHeader from '../components/visitors/VisitorsHeader'
import { withRouter } from 'react-router-dom'
import VisitorList from "../components/visitors/VisitorList";
import {getVisitorsForBusiness} from "../actions/visitorActions";


class Visitors extends Component {

    componentDidMount() {
        this.props.dispatch(getVisitorsForBusiness(this.props.businessId));
    }

    navigateDataSpecification() {
        console.log('clicked');
        this.props.history.push('/specification')
    }




    render() {
        return (
            <div>
                <VisitorsHeader specHandle={this.navigateDataSpecification.bind(this)} />
                <VisitorList visitors={this.props.visitors} loading={this.props.loading}/>
            </div>
        )
    }
}


const mapStateToProps = (state) => {
    return {
        businessId: state.account.userDetails.businessId,
        visitors: state.visitors.businessVisitors,
        loading: state.visitors.loadingBusinessVisitors
    }
}


export default withRouter(connect(mapStateToProps)(Visitors))
