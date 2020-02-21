import React, { Component } from 'react'
import '../hoc-styles/StaffSelect.css'
import { connect } from 'react-redux'
import { IAppState } from '../redux/store'
import StaffSearchHeader from '../components/staff-search/StaffSearchHeader'
import StaffResults from '../components/staff-search/StaffResults'

interface IStaffSelectProps {
    searchTerm: string;
    selectedId: string
}


class StaffSelect extends Component<IStaffSelectProps> {

    render() {
        return (
            <div className="staff-select-grid">
                <div className="staff-select-grid-item">
                    <StaffSearchHeader/>
                </div>
                <div className="staff-select-grid-item">
                    <StaffResults/>
                </div>
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
