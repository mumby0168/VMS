import React, { ReactElement } from 'react'
import { IStaffCurrentState } from '../../redux/actions/staffActioms'
import { Divider } from '@material-ui/core'

interface Props {
    staffMember: IStaffCurrentState | undefined;
}

export default function StaffMember({ staffMember }: Props): ReactElement {



    return (
        <React.Fragment>
            <h3>Visiting: {staffMember?.fullName}</h3>
            <Divider />
        </React.Fragment>
    )
}
