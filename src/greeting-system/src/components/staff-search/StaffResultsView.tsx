import React, { ReactElement } from 'react'
import { Paper, Container } from '@material-ui/core'
import { IStaffCurrentState } from '../../redux/actions/staffActioms'
import StaffResult from './StaffResult';



interface IStaffResultsViewProps {
    staff: IStaffCurrentState[]
    searchTerm: string;
}

export default function StaffResultsView({ staff, searchTerm }: IStaffResultsViewProps): ReactElement {


    const filterStaff = (name: string): IStaffCurrentState[] => {
        return staff.filter(s => s.fullName.includes(name));
    }


    const staffToRender = searchTerm === '' ? staff : filterStaff(searchTerm);

    const items = staffToRender.length !== 0 ? staffToRender.map((staff, index) => {
        return <StaffResult staffMemeber={staff} key={index} />
    }) : <h4>No results</h4>

    return (
        <div className="center h-100">
            <Paper className='results-grid'>
                {items}
            </Paper>
        </div>
    )
}
