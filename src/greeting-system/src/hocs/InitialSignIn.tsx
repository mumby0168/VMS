
import React from 'react'
import { Grid, Card, CardActionArea, Typography } from '@material-ui/core'
import '../hoc-styles/InitialSignIn.css'
import PeopleAltIcon from '@material-ui/icons/PeopleAlt';
import AssignmentIndIcon from '@material-ui/icons/AssignmentInd';


export const InitialSignIn = () => {
    return (
        <Grid spacing={2} container className="center root">
            <Grid item xs={2}></Grid>
            <Grid xs={4} item className="center">
                <Card className="card-button">
                    <CardActionArea className="full-height">
                        <div className="center">
                            <PeopleAltIcon fontSize="large"></PeopleAltIcon>
                            <Typography variant="h4">Visitors</Typography>
                        </div>
                    </CardActionArea>
                </Card>
            </Grid>
            <Grid xs={4} item className="center">
                <Card className="card-button">
                    <CardActionArea className="full-height">
                        <div className="center"                      >
                            <AssignmentIndIcon  fontSize="large"></AssignmentIndIcon>
                            <Typography variant="h4">Staff</Typography>
                        </div>
                    </CardActionArea>
                </Card>
            </Grid>
            <Grid item xs={2}></Grid>
        </Grid>
    )
}