import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Snackbar } from '@material-ui/core'
import Alert from '@material-ui/lab/Alert'

export default function Toast() {

    const toastState = useSelector(state => {
        return {
            isOpen: state.toast.isOpen,
            message: state.toast.message   
        }        
    })

    const dispatch = useDispatch();

    const handleClose = () => {
        dispatch({type: "REMOVE_TOAST"});
    }

    var severity = "info";
    var text = "";
    if(toastState.isOpen) {
        severity = toastState.message.failed ? "error" : "success";
        text = toastState.message.message;
    }

    console.log(toastState);


    return (
        <div>
            <Snackbar anchorOrigin={{
                vertical: 'top',
                horizontal: 'right'
            }} onClose={handleClose} autoHideDuration={3000} open={toastState.isOpen} >                
                    <Alert variant="filled" severity={severity}>{text}</Alert>                
            </Snackbar>
        </div>
    )
}
