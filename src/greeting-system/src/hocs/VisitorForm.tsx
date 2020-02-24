import React, { Component } from 'react'
import { IAppState } from '../redux/store'
import { connect } from 'react-redux'
import { IVisitorDataSpecification, visitorFormUpdatedAction } from '../redux/actions/visitorFormActions'
import { Paper, Divider, Container } from '@material-ui/core'
import { IStaffCurrentState } from '../redux/actions/staffActioms'
import StaffMember from '../components/visitor-form/StaffMember'
import VisitorEntry from '../components/visitor-form/VisitorEntry'
import { throws } from 'assert'

interface IVisitorFormProps {
    specifications: IVisitorDataSpecification[],
    errors: string[],
    loading: boolean;
    //TODO: figure out how to better avoid this
    staffMember: IStaffCurrentState | undefined;
    updateHandle: (index: number, newValue: string) => void;
}

class VisitorForm extends Component<IVisitorFormProps> {    

    componentDidMount() {
        //TODO: Load form spec from API
        console.log(this.props)
    }

    render() {
        

        const entries = this.props.specifications.map((spec, index) => {
            return <VisitorEntry updateHandle={this.props.updateHandle} entry={spec} index={index} />
        })

        return (
            <Container>
            <Paper style={{textAlign: 'center', padding: '1rem'}}>
                <h1>Please enter your details</h1>
                <Divider/>
                <StaffMember staffMember={this.props.staffMember}/>
                <form>
                    {entries}
                </form>
            </Paper>
            </Container>
        )
    }
}


const mapStateToProps = (state: IAppState)  => {
    return {
        specifications: state.visitorForm.fields,
        errors: state.visitorForm.errors,
        loading: state.visitorForm.loading,
        staffMember: state.staff.states.find(s => s.id === state.staffSearch.selectedId)
    }   
}

const mapDispatch = (dispatch: any) => {
    return {
        updateHandle: (index: number, newValue: string) => dispatch(visitorFormUpdatedAction({
            index: index,
            newValue: newValue
        }))
    }
}

export default connect(mapStateToProps, mapDispatch)(VisitorForm);
