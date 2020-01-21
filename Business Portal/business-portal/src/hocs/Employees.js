import React, { Component } from 'react'
import { connect } from 'react-redux'
import {Grid} from '@material-ui/core'
import EmployeesHeader from '../components/employees/EmployeesHeader'
import EmployeesTable from '../components/employees/EmployeesTable'
import CardDialog from '../common/CardDialog'
import {closeEmployeeRecords, getLatestEmployeeRecords} from '../actions/employeeActions'
import AccessRecordList from '../components/landing/AccessRecordsList'
import Progress from '../common/Progress'

class Employees extends Component {

    componentDidMount() {
        this.props.dispatch(getLatestEmployeeRecords())
        this.title = "Records Title"
    }

    

    handleClose() {
        this.props.dispatch(closeEmployeeRecords())
    }



    render() {       
        
        console.log(this.props);

        if(this.props.accessDialog.employee !== null && this.props.accessDialog.employee !== undefined) {
            this.title = this.props.accessDialog.employee.name + " Records"            
        }        


        const content = this.props.accessDialog.loading ? <Progress message="Loading employee access records ..."/> : 
            <AccessRecordList records={this.props.accessDialog.records} />


        return (
            <Grid direction="row" container>
                <CardDialog 
                showCancel={true} 
                open={this.props.accessDialog.open} 
                handleClose={this.handleClose.bind(this)} 
                title={this.title}
                >
                    {content}
                </CardDialog>
                <EmployeesHeader/>
                <EmployeesTable loading={this.props.loading} employees={this.props.employees}/>
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
        },
        loading: state.employee.loading,
        employees: state.employee.summaries
    }
})


export default connect(mapStateToProps)(Employees)
