
import React from 'react'
import { Card, CardActionArea, Typography } from '@material-ui/core'
import '../hoc-styles/InitialSignIn.css'
import PeopleAltIcon from '@material-ui/icons/PeopleAlt';
import AssignmentIndIcon from '@material-ui/icons/AssignmentInd';


export const InitialSignIn = () => {
    return (
        <div className="button-grid">
            <div className="grid-item">

            </div>
            <div className="grid-item">
                <Card className='card-button'>
                    <CardActionArea style={{ height: '100%' }}>
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
                    <CardActionArea style={{ height: '100%' }}>
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