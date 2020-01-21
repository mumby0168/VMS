import React, { Component } from 'react'
import { connect } from 'react-redux'
import {Grid} from '@material-ui/core'
import EmployeesHeader from '../components/employees/EmployeesHeader'
import EmployeesTable from '../components/employees/EmployeesTable'
import CardDialog from '../common/CardDialog'
import {closeEmployeeRecords} from '../actions/employeeActions'

class Employees extends Component {

    componentDidMount() {
        //api calls to load initial data.
        this.title = "Records Title"
    }


    employees = [
        {},
        {},
        {},
        {},
        {},
        {},
        {},
        {},
        {},
        {},
        {},
    ]

    handleClose() {
        this.props.dispatch(closeEmployeeRecords())
    }



    render() {

        if(this.props.accessDialog.employee !== null || undefined) {
            this.title = this.props.accessDialog.employee.name + " Records"
        }


        return (
            <Grid direction="row" container>
                <CardDialog 
                showCancel={true} 
                open={this.props.accessDialog.open} 
                handleClose={this.handleClose.bind(this)} 
                title={this.title}
                >
                    <h1>Content</h1>
                </CardDialog>
                <EmployeesHeader/>
                <EmployeesTable employees={this.employees}/>
            </Grid>
        )
    }
}

const mapStateToProps = (state => {
    return {
        accessDialog: {
            loading: state.employee.access.loading,
            open: state.employee.access.isOpen,
            employee: state.employee.access.employee,
            records: state.employee.access.records
        }
    }
})


export default connect(mapStateToProps)(Employees)
