import React, { Component } from 'react'
import '../hoc-styles/StaffSelect.css'
import { connect } from 'react-redux'
import { IAppState } from '../redux/store'
import StaffSearchHeader from '../components/staff-search/StaffSearchHeader'
import { updateSearchTermAction, updateSelectionStaff } from '../redux/actions/staffSearchActions'
import StaffResultsView from '../components/staff-search/StaffResultsView'
import { IStaffCurrentState } from '../redux/actions/staffActions'
import { getStaffState } from '../redux/api/user'
import { SystemViews, viewChangedAction } from '../redux/actions/systemActions'

interface IStaffSelectProps {
    searchTerm: string;
    selectedId: string;
    searchUpdateHandle: (text: string) => void;
    staff: IStaffCurrentState[];
    loadStaffStates: (siteId: string) => void;
    currentSiteId: string;
    navigate: (view: SystemViews) => void;
    updateSelected: (id: string) => void;
}


class StaffSelect extends Component<IStaffSelectProps> {

    componentDidMount() {
        this.props.loadStaffStates(this.props.currentSiteId);
    }

    render() {
        console.log(this.props)
        return (
            <div className="staff-select-grid">
                <div className="staff-select-grid-item">
                    <StaffSearchHeader searchText={this.props.searchTerm}
                    updateSearchHandle={this.props.searchUpdateHandle}/>
                </div>
                <div className="staff-select-grid-item">
                    <StaffResultsView navigate={this.props.navigate} updateSelected={this.props.updateSelected} searchTerm={this.props.searchTerm}
                    staff={this.props.staff}/>
                </div>
            </div>
        )
    }
}

const mapStateToProps = (state: IAppState) => {
    return {
        searchTerm: state.staffSearch.searchTerm,
        selectedId: state.staffSearch.selectedId,
        staff: state.staff.states,
        currentSiteId: state.system.site.id
    }
}

const mapDispatch = (dispatch: any) => {
    return {
        searchUpdateHandle: (text: string) => dispatch(updateSearchTermAction(text)),
        loadStaffStates: (siteId: string) => dispatch(getStaffState(siteId)),
        navigate: (view: SystemViews) => dispatch(viewChangedAction(view)),
        updateSelected: (id: string) => dispatch(updateSelectionStaff(id)),
    }
}

export default connect(mapStateToProps, mapDispatch)(StaffSelect)
