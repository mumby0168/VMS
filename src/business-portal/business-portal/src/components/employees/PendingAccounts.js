import React from 'react'
import CardDialog from '../../common/CardDialog'
import { hidePending, removePendingAccount } from '../../actions/employeeActions';
import { List, ListItem, ListItemText, ListItemSecondaryAction, IconButton } from '@material-ui/core';
import Progress from '../../common/Progress';
import { Delete } from '@material-ui/icons';
import Alert from '@material-ui/lab/Alert';

export default function PendingAccounts(props) {

    console.log(props);
    const {accounts, isOpen, loading, dispatch, removing, error} = props;

    const handleClose = function() {
        dispatch(hidePending());
    }

    const inital = loading ? <Progress message="Loading accounts"/> : accounts.map((account, index) => {
        return (
            <ListItem key={index}>
                <ListItemText primary={account.emailAddress}></ListItemText>
                <ListItemSecondaryAction>
                    <IconButton onClick={(e) => dispatch(removePendingAccount(account.id))}>
                        <Delete/>
                    </IconButton>
                </ListItemSecondaryAction>
            </ListItem>
        )
    });

    const content = removing ? <Progress message="Remove account"/> : inital;

    const errorContent = error !== "" ? <Alert severity="error">{error}</Alert> : ""


    return (
        <CardDialog text="The currently un-completed accounts" showCancel handleClose={handleClose} open={isOpen} title="Pending Accounts">
            {errorContent}
            <List>
                {content}
            </List>
        </CardDialog>
    )
}
