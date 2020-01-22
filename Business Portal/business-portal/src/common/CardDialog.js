import React from 'react'
import { Dialog, DialogTitle, DialogContent, DialogActions, DialogContentText, Button, Divider } from '@material-ui/core'

export default function CardDialog(props) {


    const cancel = props.showCancel ? <Button variant="contained" onClick={props.handleClose}>Cancel</Button> : ""

    return (
        <Dialog maxWidth={props.maxWidth} fullWidth onClose={props.handleClose} open={props.open}>
            <DialogTitle>
                {props.title}
                <Divider/>
            </DialogTitle>
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
