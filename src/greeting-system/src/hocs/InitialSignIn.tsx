
import React from 'react'
import { Card, CardActionArea, Typography } from '@material-ui/core'
import '../hoc-styles/InitialSignIn.css'
import PeopleAltIcon from '@material-ui/icons/PeopleAlt';
import AssignmentIndIcon from '@material-ui/icons/AssignmentInd';
import { SystemViews } from '../redux/actions/systemActions';


interface IInitSignInProps {
    naviagate: (view : SystemViews) => void;
}


export const InitialSignIn = (props: IInitSignInProps) => {
    return (
        <div className="button-grid">
            <div className="grid-item">

            </div>
            <div className="grid-item">
                <Card className='card-button'>
                    <CardActionArea onClick={(e) => props.naviagate(SystemViews.STAFF_SELECT)} style={{ height: '100%' }}>
                        <div className="ta">
                            <PeopleAltIcon style={{ fontSize: 100 }}></PeopleAltIcon>
                            <Typography variant="h2">
                                Visitor
                            </Typography>
                        </div>
                    </CardActionArea>
                </Card>
            </div>
            <div className="grid-item">
                <Card className='card-button'>
                    <CardActionArea onClick={(e) => props.naviagate(SystemViews.STAFF_KEYPAD)} style={{ height: '100%' }}>
                        <div className="ta">
                            <AssignmentIndIcon style={{ fontSize: 100 }}/>
                            <Typography variant="h2">
                                Staff
                            </Typography>
                        </div>
                    </CardActionArea>
                </Card>
            </div>
            <div className="grid-item">

            </div>
        </div>
    )
}