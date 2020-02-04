import React, { Component } from 'react'
import { Grid } from '@material-ui/core'
import VisitorSpecHeader from '../components/visitor-spec/VisitorSpecHeader'
import VisitorSpecTable from '../components/visitor-spec/VisitorSpecTable'
import { withRouter } from 'react-router-dom'
import { connect } from 'react-redux'
import { getBusinessSpecifications, showAdd, getVaidationOptions } from '../actions/specificationActions'
import AddUserSpec from '../components/visitor-spec/AddDataSpec'
import { StarRate } from '@material-ui/icons'

class VisitorSpec extends Component {

    componentDidMount() {
        this.props.dispatch(getBusinessSpecifications());
    }

    openAdd() {
        this.props.dispatch(getVaidationOptions())
        this.props.dispatch(showAdd());        
    }


    render() {
        return (
            <div>
                <AddUserSpec form={this.props.add.form} options={this.props.add.options} open={this.props.add.open}/>
                <Grid direction="row" container>
                    <VisitorSpecHeader openAdd={this.openAdd.bind(this)} />
                    <VisitorSpecTable loading={this.props.loading} specifications={this.props.specifications} />
                </Grid>
            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        specifications: state.specs.specifications,
        loading: state.specs.loadingSpecs,
        add: {
            open: state.specs.add.open,
            options: state.specs.add.options,
            form: state.specs.add.form
        }
    }
}

export default withRouter(connect(mapStateToProps)(VisitorSpec))


