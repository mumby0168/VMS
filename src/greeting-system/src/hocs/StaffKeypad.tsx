import React, { Component } from 'react'
import '../hoc-styles/StaffKeypad.css'
import Keypad from '../components/staff-keypad/Keypad'
import { connect } from 'react-redux'
import { IAppState } from '../redux/store'
import { Card, Typography } from '@material-ui/core'
import { viewChangedAction, SystemViews } from '../redux/actions/systemActions'
import { updateCodeAction } from '../redux/actions/staffKeypadActions'
import { updateOverlayAction, IconType, closeOverlay, openOverlay } from '../redux/actions/overlayActions'
import { getStaffState } from '../redux/api/user'
import { IStaffCurrentState } from '../redux/actions/staffActioms'

interface IStaffKeypadProps {   
    staffCode: string;
    handleSucessfulSignIn: (staffState: IStaffCurrentState) => void;
    handleSignInFailure: (reason: string) => void;
    loadCurrentStaffState: (siteId: string) => void;
    currentSiteId: string;
    staffStates: IStaffCurrentState[];
}


class StaffKeypad extends Component<IStaffKeypadProps> {
    
    componentDidMount() {
        this.props.loadCurrentStaffState(this.props.currentSiteId);
    }

    render() {
        return (
            <div className="staff-keypad-grid">
                <div className="staff-keypad-item">
                    <div className="number-card">
                    <Card className='ta h-50'>                        
                        <Typography variant="h3">
                            {this.props.staffCode !== "" ? this.props.staffCode : "Enter Code"}
                        </Typography>                        
                    </Card>
                    </div>
                </div>
                <div className="staff-keypad-item">
                    <Keypad 
                    states={this.props.staffStates}
                    siteId={this.props.currentSiteId}
                    handleSucessfulSignIn={this.props.handleSucessfulSignIn}
                    handleSignInFailure={this.props.handleSignInFailure}
                    code={this.props.staffCode}/>
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state: IAppState) => {
    return {
        staffCode: state.staffKeypad.staffCode,
        currentSiteId: state.system.site.id,       
        staffStates: state.staff.states 
    }
}

const mapDispatchToProps = (dispatch: any) => {
    return {
        handleSucessfulSignIn: (staffState: IStaffCurrentState) => {
            dispatch(updateOverlayAction(openOverlay(`${staffState.fullName} Signed ${staffState.action === "in" ? "out" : "in"} succesfully`, IconType.TICK)));
            setTimeout(() => {
                dispatch(updateOverlayAction(closeOverlay()));
                dispatch(updateCodeAction(""));
                dispatch(viewChangedAction(SystemViews.INIT_SIGN_IN));
                
            }, 1000)
            
        },
        handleSignInFailure: (reason: string) => dispatch(updateOverlayAction(openOverlay(reason, IconType.ERROR, false, true))),
        loadCurrentStaffState: (siteId: string) => dispatch(getStaffState(siteId)),
    }
}


export default connect(mapStateToProps, mapDispatchToProps)(StaffKeypad)
