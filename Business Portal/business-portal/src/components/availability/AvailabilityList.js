import React from 'react';
import AvailabilityTemplate from './AvailabilityTemplate';
import { Grid, makeStyles } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
    root: {
        padding: theme.spacing(2),
        height: '90%',
        overflowY: 'auto'        
    }
}));

export default function AvailabilityList(props) {   

    const classes = useStyles();
    
    console.log(props.availability);

    const available = props.availability !== null ?  props.availability.users.map((user, index) => {
        return <AvailabilityTemplate key={index} user={user}/>
    }) : <h2>No Info</h2>

    return <Grid className={classes.root} spacing={2} container>{available}</Grid>
}