
import React from 'react'
import { Card, CardActionArea, Typography } from '@material-ui/core'
import '../hoc-styles/InitialSignIn.css'
import { SystemViews } from '../redux/actions/systemActions';
import ExpandLessIcon from '@material-ui/icons/ExpandLess';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
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
                            <Typography variant="h2">
                                SIGN IN
                            </Typography>
                            <ExpandLessIcon style={{ fontSize: 100, color: 'green' }}/>
                        </div>
                    </CardActionArea>
                </Card>
            </div>
            <div className="grid-item">
                <Card className='card-button'>
                    <CardActionArea onClick={(e) => props.naviagate(SystemViews.VISITOR_OUT)} style={{ height: '100%' }}>
                        <div className="ta">
                            <Typography variant="h2">
                                SIGN OUT
                            </Typography>
                            <ExpandMoreIcon style={{ fontSize: 100, color: 'red' }}/>
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
                                    STAFF
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