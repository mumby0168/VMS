import React from 'react'
import CardDialog from '../../common/CardDialog'
import { hidePending } from '../../actions/employeeActions';
import { List, ListItem, ListItemText, ListItemSecondaryAction, IconButton } from '@material-ui/core';
import Progress from '../../common/Progress';
import { Delete } from '@material-ui/icons';

export default function PendingAccounts(props) {

    console.log(props);
    const {accounts, isOpen, loading, dispatch} = props;

    const handleClose = function() {
        dispatch(hidePending());
    }

    const content = loading ? <Progress message="Loading Accoutns"/> : accounts.map((account, index) => {
        return (
            <ListItem key={index}>
                <ListItemText primary={account.emailAddress}></ListItemText>
                <ListItemSecondaryAction>
                    <IconButton>
                        <Delete/>
                    </IconButton>
                </ListItemSecondaryAction>
            </ListItem>
        )
    });


    return (
        <CardDialog text="The currently un-completed accounts" showCancel handleClose={handleClose} open={isOpen} title="Pending Accounts">
            <List>
                {content}
            </List>
        </CardDialog>
    )
}
