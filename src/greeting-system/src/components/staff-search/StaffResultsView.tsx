import React, { ReactElement } from 'react'
import { Paper } from '@material-ui/core'
import { IStaffCurrentState } from '../../redux/actions/staffActioms'
import StaffResult from './StaffResult';
import { SystemViews } from '../../redux/actions/systemActions';



interface IStaffResultsViewProps {
    staff: IStaffCurrentState[]
    searchTerm: string;
    navigate: (view: SystemViews) => void;
    updateSelected: (id: string) => void;
}

export default function StaffResultsView({ staff, searchTerm, navigate, updateSelected }: IStaffResultsViewProps): ReactElement {


    const filterStaff = (name: string): IStaffCurrentState[] => {
        return staff.filter(s => s.fullName.toLowerCase().includes(name));
    }


    const staffToRender = searchTerm === '' ? staff : filterStaff(searchTerm);

    const items = staffToRender.length !== 0 ? staffToRender.map((staff, index) => {
        return <StaffResult navigate={navigate} updateSelected={updateSelected} staffMemeber={staff} key={index} />
    }) : <h4>No results</h4>

    return (
        <div className="center h-100">
            <Paper className='results-grid'>
                {items}
            </Paper>
        </div>
    )
}
