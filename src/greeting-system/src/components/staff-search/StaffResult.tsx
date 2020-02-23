import React, { ReactElement } from 'react'
import { IStaffCurrentState } from '../../redux/actions/staffActioms'
import { Card, CardActionArea, Avatar, Divider, Chip } from '@material-ui/core'

interface IStaffResultProps {
    staffMemeber: IStaffCurrentState
}

export default function StaffResult({ staffMemeber }: IStaffResultProps): ReactElement {



    return (
        <Card className='staff-result-wrapper' variant='outlined'>
            <CardActionArea className='h-100'>
                <div className='results-inner-wrapper'>
                    <div>
                        <div className="center avatar-wrapper">
                            <Avatar
                                style={{
                                    height: 70,
                                    width: 70
                                }}
                                >{staffMemeber.initials}</Avatar>
                        </div>
                        <Divider className='divider-result' />
                        <h2>{staffMemeber.fullName}</h2>
                        <div className='center'>
                            <Chip color='primary' label='Select'></Chip>
                        </div>
                    </div>
                </div>
            </CardActionArea>
        </Card>
    )
}
