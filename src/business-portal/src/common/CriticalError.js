import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Backdrop, Typography, makeStyles, Button } from '@material-ui/core'
import { hideCriticalError } from '../actions/uiActions';
import { useHistory } from 'react-router-dom';
import { logout } from '../actions/accountActions';

const useStyles = makeStyles((theme) => ({
    backdrop: {
        zIndex: 255
    },
    card: {
        padding: '15%'
    },
    items: {
        paddingBottom: theme.spacing(1)
    }
}));

export default function CriticalError(props) {

    const history = useHistory();

    const classes = useStyles();

    const dispatch = useDispatch();    

    const state = useSelector(state => {
        return {
            visible: state.ui.critical.visible,
            message: state.ui.critical.message
        }        
    });    

    const handleClose = () => {
        dispatch(hideCriticalError());     
        dispatch(logout());
        history.push('/login');
    }

    
    return (
        <Backdrop className={classes.backdrop} open={state.visible}>
            <div align="center">
                <Typography className={classes.items} variant="h3">Oops ! </Typography>                
                <Typography className={classes.items} variant="h4">{state.message}</Typography>
                <Typography className={classes.items} variant="h6">Unfortunately we cannot recover from this.</Typography>
                <Button className={classes.items} variant="contained" color="secondary" onClick={handleClose}>Exit Application</Button>
            </div>
        </Backdrop>
    )
}
