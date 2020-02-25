import React, { Component } from 'react'
import { IAppState } from '../redux/store'
import { connect } from 'react-redux'
import { IVisitorDataSpecification, visitorFormUpdatedAction, updateErrorsAction } from '../redux/actions/visitorFormActions'
import { Paper, Divider, Container, Button, List, ListItem } from '@material-ui/core'
import { IStaffCurrentState } from '../redux/actions/staffActioms'
import StaffMember from '../components/visitor-form/StaffMember'
import VisitorEntry from '../components/visitor-form/VisitorEntry'
import { isEmailValid, isPostCodeValid } from '../services/validation'
import { SystemViews, viewChangedAction } from '../redux/actions/systemActions'
import { openOverlay, IconType, closeOverlay, updateOverlayAction } from '../redux/actions/overlayActions'
import { Alert } from '@material-ui/lab'
import {getVisitorFormSpecifications} from '../redux/api/visitors'
import '../hoc-styles/VisitorForm.css'

interface IVisitorFormProps {
    specifications: IVisitorDataSpecification[],
    errors: string[],
    loading: boolean;
    //TODO: figure out how to better avoid this
    staffMember: IStaffCurrentState | undefined;
    updateHandle: (index: number, newValue: string) => void;
    updateErrors: (errors: string[]) => void;    
    submitForm: (data: IVisitorDataSpecification[]) => void;
    getFormData: () => void;
    showLoading: (message: string) => void;
    closeLoading: () => void;
}

class VisitorForm extends Component<IVisitorFormProps> {    

    componentDidMount() {
        this.props.getFormData();
    }

    handleSubmit(e: any) {
        console.log('submitted')
        e.preventDefault();
        
        const errors: string[] = []

        for (let i = 0; i < this.props.specifications.length; i++) {
            const spec = this.props.specifications[i];
            switch (spec.validationCode) {
                case "Required":
                    if(spec.value === '') errors.push(spec.validationMessage);
                    break;
                case "Email":
                    if(!isEmailValid(spec.value)) errors.push(spec.validationMessage);
                    break;
                case "Post Code":
                    if(!isPostCodeValid(spec.value)) errors.push(spec.validationMessage);
                    break;
                default:
                    return;
            }                  
        }      

        if(errors.length !== 0)
        {
            this.props.updateErrors(errors);
            return;
        } 
        
        this.props.updateErrors([]);
        
        //TODO: submit form to API.


        for (let i = 0; i < this.props.specifications.length; i++) {            
            this.props.updateHandle(i, '');            
        }        
        this.props.submitForm(this.props.specifications);
    }

    render() {

        this.props.loading ? this.props.showLoading('Loading ...') :
        this.props.closeLoading()
        

        const entries = this.props.specifications.map((spec, index) => {
            return <VisitorEntry key={index} updateHandle={this.props.updateHandle} entry={spec} index={index} />
        })

        const errors = this.props.errors.map((err, i) => {
            return (
            <ListItem key={i}>
                <Alert className='error-alert' variant='filled' severity='error'>{err}</Alert>
            </ListItem>)
        });

        return (
            <Container className='center root'>
            <Paper className='root-inner'>
                <h1>Please enter your details</h1>
                <Divider/>
                <StaffMember staffMember={this.props.staffMember}/>

                <div className="center">
                <List>
                    {errors}
                </List>
                </div>                

                <form  onSubmit={this.handleSubmit.bind(this)}>
                    {entries}
                    <Button className='visitor-submit' type='submit' variant='contained' color='primary'>Submit</Button>
                </form>
            </Paper>
            </Container>
        )
    }
}

const handleFormSubmit = (dispatch: any, data: IVisitorDataSpecification[]) => {
    console.log('1');
    dispatch(updateOverlayAction(openOverlay('Submitting your data', IconType.NONE, true)));

    console.log('2');
    //TODO: simulating success case
    setTimeout(() => {
        dispatch(updateOverlayAction(openOverlay('Thank you! Enjoy your visit.', IconType.TICK)));
    }, 1000);
        
    setTimeout(() => {
        dispatch(updateOverlayAction(closeOverlay()));
        dispatch(viewChangedAction(SystemViews.INIT_SIGN_IN));
    }, 3000)
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
        })),
        updateErrors: (errors: string[]) => dispatch(updateErrorsAction(errors)),        
        submitForm: (data: IVisitorDataSpecification[]) => handleFormSubmit(dispatch, data),
        getFormData: () => dispatch(getVisitorFormSpecifications()),
        showLoading: (message: string) => dispatch(updateOverlayAction(openOverlay(message, IconType.NONE, true))),
        closeLoading: () => dispatch(updateOverlayAction(closeOverlay()))
    }
}



export default connect(mapStateToProps, mapDispatch)(VisitorForm);
