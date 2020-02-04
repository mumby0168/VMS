import React, { Component } from 'react'
import { Grid } from '@material-ui/core'
import VisitorSpecHeader from '../components/visitor-spec/VisitorSpecHeader'
import VisitorSpecTable from '../components/visitor-spec/VisitorSpecTable'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { getBusinessSpecifications } from '../actions/specificationActions'

class VisitorSpec extends Component {

    componentDidMount() {
        this.props.dispatch(getBusinessSpecifications());
    }


    render() {
        return (
            <Grid direction="row" container>
                <VisitorSpecHeader/>
                <VisitorSpecTable loading={this.props.loading} specifications={this.props.specifications}/>
            </Grid>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        specifications: state.specs.specifications,
        loading: state.specs.loadingSpecs
    }
}

export default withRouter(connect(mapStateToProps)(VisitorSpec))


