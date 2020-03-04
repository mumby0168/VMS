
import React from 'react'
import { Card, CardActionArea, Typography } from '@material-ui/core'
import '../hoc-styles/InitialSignIn.css'
import { SystemViews } from '../redux/actions/systemActions';
import EventAvailableIcon from '@material-ui/icons/EventAvailable';
import EventBusyIcon from '@material-ui/icons/EventBusy';
import SupervisedUserCircleIcon from '@material-ui/icons/SupervisedUserCircle';


interface IInitSignInProps {
    naviagate: (view : SystemViews) => void;
}


export const InitialSignIn = (props: IInitSignInProps) => {
    return (
        <div className='center h-100'>
        <div className="button-grid">
            <div className="grid-item">

            </div>
            <div className="grid-item">
                <Card className='card-button'>
                    <CardActionArea onClick={(e) => props.naviagate(SystemViews.STAFF_SELECT)} style={{ height: '100%' }}>
                        <div className="ta">
                            <EventAvailableIcon style={{ fontSize: 100, color: 'green' }}></EventAvailableIcon>
                            <Typography variant="h2">
                                Sign In
                            </Typography>
                        </div>
                    </CardActionArea>
                </Card>
            </div>
            <div className="grid-item">
                <Card className='card-button'>
                    <CardActionArea onClick={(e) => props.naviagate(SystemViews.VISITOR_OUT)} style={{ height: '100%' }}>
                        <div className="ta">
                            <EventBusyIcon style={{ fontSize: 100, color: 'red' }}/>
                            <Typography variant="h2">
                                Sign Out
                            </Typography>
                        </div>
                    </CardActionArea>
                </Card>
            </div>
            <div className="grid-item">

            </div>
            <div className="grid-item">

            </div>

            <div className="grid-item staff-sign-in">
                    <Card className="card-button">
                        <CardActionArea onClick={(e) => props.naviagate(SystemViews.STAFF_KEYPAD)} style={{ height: '100%' }}>
                            <div className="ta">
                                <SupervisedUserCircleIcon style={{ fontSize: 75}}/>
                                <Typography variant="h3">
                                    Staff
                                </Typography>
                            </div>
                        </CardActionArea>
                    </Card>
            </div>

            <div className="grid-item">

            </div>

        </div>
        </div>
    )
}