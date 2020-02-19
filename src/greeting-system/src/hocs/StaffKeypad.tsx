import React, { Component } from 'react'
import '../hoc-styles/StaffKeypad.css'
import Keypad from '../components/staff-keypad/Keypad'
import { connect } from 'react-redux'
import { IAppState } from '../redux/store'
import { Card, Typography } from '@material-ui/core'

interface IStaffKeypadProps {   
    staffCode: string;
}


class StaffKeypad extends Component<IStaffKeypadProps> {
    

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
                    <Keypad code={this.props.staffCode}/>
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state: IAppState) => {
    return {
        staffCode: state.staffKeypad.staffCode,
    }
}


export default connect(mapStateToProps)(StaffKeypad)
