import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Backdrop, Typography, makeStyles, Button } from '@material-ui/core'
import { hideCriticalError } from '../actions/uiActions';
import { useHistory } from 'react-router-dom';
import { logout } from '../actions/accountActions';

const useStyles = makeStyles(() => ({
    backdrop: {
        zIndex: 255
    },
    card: {
        padding: '15%'
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
                <Typography variant="h3">Oops ! </Typography>
                <Typography variant="h4">Something went wrong in which we cannot recover :(</Typography>
                <Typography variant="h6">{state.message}</Typography>
                <Button variant="contained" color="secondary" onClick={handleClose}>Close</Button>
            </div>
        </Backdrop>
    )
}
