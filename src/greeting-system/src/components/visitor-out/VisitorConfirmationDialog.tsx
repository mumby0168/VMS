import React from 'react'
import {DialogActions, DialogContentText, Button, Dialog, DialogContent, DialogTitle} from "@material-ui/core";

interface IVisitorConfirmationDialogProps {
    id: string;
    name: string;
    open: boolean;
    confirmHandle: (id: string, name: string) => void;
    cancelHandle: () => void;
}

export default function VisitorConfirmationDialog({id, name, open, confirmHandle, cancelHandle}: IVisitorConfirmationDialogProps) {
    return (
        <Dialog
            open={open}
            onClose={cancelHandle}
            aria-labelledby="alert-dialog-title"
            aria-describedby="alert-dialog-description"
        >
            <DialogTitle id="alert-dialog-title">{name + " sign out"}</DialogTitle>
            <DialogContent>
                <DialogContentText id="alert-dialog-description">
                    This will sign you of the system and will effect fire evacuation lists.
                </DialogContentText>
            </DialogContent>
            <DialogActions>
                <Button onClick={cancelHandle} color="primary">
                    Cancel
                </Button>
                <Button onClick={(e) => confirmHandle(id, name)} color="primary" autoFocus>
                    Confirm
                </Button>
            </DialogActions>
        </Dialog>
    )
}