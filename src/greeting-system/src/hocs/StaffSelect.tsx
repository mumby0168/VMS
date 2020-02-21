import React, { Component } from 'react'
import '../hoc-styles/StaffSelect.css'
import { connect } from 'react-redux'
import { IAppState } from '../redux/store'

interface IStaffSelectProps {
    searchTerm: string;
    selectedId: string
}


class StaffSelect extends Component<IStaffSelectProps> {

    render() {
        return (
            <div className="h-100">
                Staff Select 
            </div>
        )
    }
}

const mapStateToProps = (state: IAppState) => {
    return {
        searchTerm: state.staffSearch.searchTerm,
        selectedId: state.staffSearch.selectedId
    }
}

const mapDispatch = () => {
    return {

    }
}

export default connect(mapStateToProps, mapDispatch)(StaffSelect)
