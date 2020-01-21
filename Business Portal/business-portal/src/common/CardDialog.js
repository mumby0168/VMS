import React from 'react'
import { Dialog, DialogTitle, DialogContent, DialogActions, DialogContentText, Button } from '@material-ui/core'

export default function CardDialog(props) {


    const cancel = props.showCancel ? <Button onClick={props.handleClose}>Cancel</Button> : ""

    return (
        <Dialog maxWidth="md" fullWidth onClose={props.handleClose} open={props.open}>
            <DialogTitle>{props.title}</DialogTitle>
            <DialogContent>
                <DialogContentText>
                    {props.text}
                </DialogContentText>
                {props.children}
            </DialogContent>
            <DialogActions>
                {cancel}
                {props.actions}                
            </DialogActions>
        </Dialog>
    )
}
