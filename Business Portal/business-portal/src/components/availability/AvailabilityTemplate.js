import React from 'react';
import { Avatar, Card, Grid,CardContent, Typography, makeStyles, Divider, CardActions, IconButton, Tooltip } from "@material-ui/core";
import PhoneIcon from '@material-ui/icons/Phone';
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
    },
    shapeActive: {
        backgroundColor: '#00e676',
        width: 25,
        height: 25,
        border: '1px solid #00e676',
        borderRadius: '15px'
      },
      shapeInative: {
        backgroundColor: '#ff1744',
        width: 25,
        height: 25,
        border: '1px solid #ff1744',
        borderRadius: '15px'
      },
  }));

export default function AvailabilityTemplate(props) {

    const classes = useStyles();

    const squareClass = props.user.status === "in" ? classes.shapeActive : classes.shapeInative;  
    const toolTip = props.user.status === "in" ? "In" : "Out";        
    const rectangle = <Tooltip title={toolTip}><div className={squareClass}/></Tooltip>

    const copyEmail = () => {
        navigator.clipboard.writeText(props.user.email);
    }

    const copyPhone = () => {
        navigator.clipboard.writeText(props.user.contactNumber);        
    }

    return (
        <Grid item xs={12} md={3}>
            <Card variant="outlined">
                <CardContent className={classes.content}>                                                                                                    
                <div style={{position: 'relative'}}>
                    <div style={{position: 'absolute', top: '3px', right: '3px'}}>                    
                        {rectangle}                    
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
                    <Tooltip title="Copy Email Address">
                        <IconButton onClick={copyEmail} variant="contained" color="secondary">
                            <MailIcon/>
                        </IconButton>
                    </Tooltip>
                    <Tooltip title="Copy Phone Number">
                        <IconButton onClick={copyPhone} variant="contained" color="secondary">
                            <PhoneIcon/>
                        </IconButton>
                    </Tooltip>
                </CardActions>
            </Card>
        </Grid>
    )
}