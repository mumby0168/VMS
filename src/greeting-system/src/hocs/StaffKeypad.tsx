import React, { Component } from 'react'
import '../hoc-styles/StaffKeypad.css'
import Keypad from '../components/staff-keypad/Keypad'
import { connect } from 'react-redux'
import { IAppState } from '../redux/store'

interface IStaffKeypadProps {   
    staffCode: string;
}


class StaffKeypad extends Component<IStaffKeypadProps> {
    

    render() {
        return (
            <div className="staff-keypad-grid">
                <div className="staff-keypad-item">Number Area</div>
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
