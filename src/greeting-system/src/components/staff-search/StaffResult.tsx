import React, { ReactElement } from 'react'
import { IStaffCurrentState } from '../../redux/actions/staffActioms'
import { Card, CardActionArea, Avatar, Divider, Chip } from '@material-ui/core'
import { SystemViews } from '../../redux/actions/systemActions';

interface IStaffResultProps {
    staffMemeber: IStaffCurrentState
    navigate: (view: SystemViews) => void;
    updateSelected: (id: string) => void;
}

export default function StaffResult({ staffMemeber, navigate, updateSelected }: IStaffResultProps): ReactElement {
    

    const handleClick = () => {        
        updateSelected(staffMemeber.id);
        navigate(SystemViews.VISITOR_FORM);
    }

    return (
        <Card  className='staff-result-wrapper' variant='outlined'>
            <CardActionArea onClick={handleClick} className='h-100'>
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
