import React, { ReactElement } from 'react'
import { Paper } from '@material-ui/core'
import { IStaffCurrentState } from '../../redux/actions/staffActioms'
import StaffResult from './StaffResult';

interface IStaffResultsViewProps {
    staff: IStaffCurrentState[]
    searchTerm: string;
}

export default function StaffResultsView({staff, searchTerm}: IStaffResultsViewProps): ReactElement {


    const filterStaff = (name: string): IStaffCurrentState[] => {
        return [];
    }


    const staffToRender = searchTerm === '' ? staff : filterStaff(searchTerm);

    const items = staffToRender.map((staff, index) => {
        return <StaffResult staffMemeber={staff} key={index} />
    })

    return (
        <div className='center h-100'>
            <Paper  className='results-grid'>
                {items}
            </Paper>
        </div>
    )
}
