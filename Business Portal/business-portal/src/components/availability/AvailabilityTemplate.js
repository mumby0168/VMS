import React from 'react';
import { Avatar, Card, Grid, CardContent, Typography, makeStyles, Divider, Chip, CardActions, Button, IconButton } from "@material-ui/core";
import { green, red } from '@material-ui/core/colors';
import RadioButtonCheckedIcon from '@material-ui/icons/RadioButtonChecked';
import MailIcon from '@material-ui/icons/Mail';

const useStyles = makeStyles(theme => ({        
    large: {
      width: theme.spacing(12),
      height: theme.spacing(12),
      marginBottom: theme.spacing(2)
    },
    items: {
        marginBottom: theme.spacing(0.5)
    },
    content: {
        padding: theme.spacing(1),
        paddingBottom: '0px'        
    }
  }));

export default function AvailabilityTemplate(props) {

    const classes = useStyles();

    console.log(props.user);
    const color = props.user.status === "in" ? 'green' : 'red';    
    const text = props.user.status === "in" ? "Online" : "Offline";

    console.log(color);

    return (
        <Grid item xs={12} md={4}>
            <Card>
                <CardContent className={classes.content}>                                                                                                    
                <div style={{position: 'relative'}}>
                    <div style={{position: 'absolute', top: '0px', left: '0px'}}>
                    <RadioButtonCheckedIcon style={{color: color}}/>                                       
                    </div>           

                    <div align="center">
                        <Avatar className={classes.large}>{props.user.initials}</Avatar>
                        {/* <Chip className={classes.item} label={text} color="primary"/> */}                        
                        <Typography variant="h5" className={classes.items} >{props.user.name}</Typography>    
                        <Divider className={classes.items} />
                        <Typography className={classes.items} variant="subtitle1" gutterBottom>
                            {props.user.email}
                        </Typography>   
                        <Typography variant="subtitle1" gutterBottom>
                            {props.user.contactNumber}
                        </Typography>                                      
                    </div>
                </div>                            
                </CardContent>
                <CardActions>
                    <IconButton variant="contained" color="secondary">
                        <MailIcon></MailIcon>
                    </IconButton>
                </CardActions>
            </Card>
        </Grid>
    )
}