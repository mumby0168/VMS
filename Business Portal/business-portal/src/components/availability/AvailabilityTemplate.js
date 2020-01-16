import React from 'react';
import { Avatar, Card, Grid, CardContent, Typography, CircularProgress, makeStyles } from "@material-ui/core";
import { green, red } from '@material-ui/core/colors';
import RadioButtonCheckedIcon from '@material-ui/icons/RadioButtonChecked';

const useStyles = makeStyles(theme => ({        
    large: {
      width: theme.spacing(7),
      height: theme.spacing(7),
    },
  }));

export default function AvailabilityTemplate(props) {

    const clasess = useStyles();

    console.log(props.user);
    const color = props.user.status === "in" ? green : red;    

    console.log(color);

    return (
        <Grid item xs={12} md={6}>
            <Card>
                <CardContent>
                    <Grid container orientation="horizontal">
                        <Grid item xs={2}>
                        <RadioButtonCheckedIcon style={{top: '0.5rem', left: '0.5rem', color: color[500]}}/>
                        </Grid>
                        <Grid item xs={10}>
                            <div align="center">
                            <Avatar className={clasess.large}>{props.user.initials}</Avatar>
                            <Typography>{props.user.name}</Typography>                        
                    </div>        
                        </Grid>
                    </Grid>                    
                    
                </CardContent>
            </Card>
        </Grid>
    )
}