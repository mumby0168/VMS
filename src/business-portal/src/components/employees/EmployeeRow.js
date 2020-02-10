import React from 'react'
import { Avatar, Typography, Fab, Tooltip, TableRow, TableCell, makeStyles } from '@material-ui/core'
import WatchLaterIcon from '@material-ui/icons/WatchLater';
import { useDispatch } from 'react-redux';
import { openEmployeeRecords, getEmployeeAccessRecords } from '../../actions/employeeActions';
import { Delete } from '@material-ui/icons';



const useStyles = makeStyles(theme => ({
    avatar: {
        marginBottom: theme.spacing(0.5)
    },
    actionButtons: {
        marginLeft: theme.spacing(0.5)
    }
}));

export default function EmployeeRow(props) {

    const dispatch = useDispatch();

    const classes = useStyles();

    const showDialog = () => {
        console.log(props.employee);
        dispatch(openEmployeeRecords(props.employee))
        dispatch(getEmployeeAccessRecords(props.employee.id));
    }

    const handleRemove = (id) => {
        console.log(id);
        props.showConfirm(id);
    }

    return (
        <TableRow>
            <TableCell>                
                <Avatar className={classes.avatar}>{props.employee.initials}</Avatar>                                    
            </TableCell>
            <TableCell>
                <Typography>{props.employee.name}</Typography>                                    
            </TableCell>
            <TableCell>
                <Typography>{props.employee.siteName}</Typography>
            </TableCell>
            <TableCell>
                <Typography>{`${props.employee.lastAction} (${props.employee.lastTime})`}</Typography>
            </TableCell>
            <TableCell>
                <Tooltip className={classes.actionButtons} title="View Records">
                    <Fab onClick={showDialog} color="primary">
                        <WatchLaterIcon></WatchLaterIcon>
                    </Fab>
                </Tooltip>
                <Tooltip className={classes.actionButtons} title="Remove Employee"> 
                    <Fab onClick={(e) => handleRemove(props.employee.accountId)} color="primary">
                        <Delete/>
                    </Fab>
                </Tooltip>
            </TableCell>
        </TableRow>
    )
}
