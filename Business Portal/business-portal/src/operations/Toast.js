import React from 'react'
import { useSelector, useDispatch } from 'react-redux'
import { Snackbar, Slide } from '@material-ui/core'
import Alert from '@material-ui/lab/Alert'
import AlertTitle from '@material-ui/lab/AlertTitle'

export default function Toast() {

    const toastState = useSelector(state => {
        return {
            isOpen: state.toast.isOpen,
            message: state.toast.message
        }
    })

    const dispatch = useDispatch();

    const handleClose = () => {
        dispatch({ type: "REMOVE_TOAST" });
    }

    var severity = "info";
    var text = "";
    var title = "";
    if (toastState.isOpen) {
        severity = toastState.message.failed ? "error" : "success";
        text = toastState.message.message;
        title = toastState.message.failed ? "Error" : "Success";
    }

    console.log(toastState);


    return (
        <div>

            <Snackbar
                anchorOrigin={{
                    vertical: 'bottom',
                    horizontal: 'right'
                }} onClose={handleClose} autoHideDuration={3000} open={toastState.isOpen} >
                <Alert style={{
                    paddingTop: '1rem',
                    paddingBottom: '1rem',
                    paddingRight: '1rem',
                }} variant="filled" severity={severity}>
                    <AlertTitle>{title}</AlertTitle>
                    {text}
                </Alert>
            </Snackbar>
        </div>
    )
}
