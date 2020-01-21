import React from 'react'
import { Avatar, Typography, Fab, Tooltip, TableRow, TableCell, makeStyles } from '@material-ui/core'
import WatchLaterIcon from '@material-ui/icons/WatchLater';
import { useDispatch } from 'react-redux';
import { openEmployeeRecords, getEmployeeAccessRecords } from '../../actions/employeeActions';



const useStyles = makeStyles(theme => ({
    avatar: {
        marginBottom: theme.spacing(0.5)
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
                <Tooltip title="View Records">
                    <Fab onClick={showDialog} color="primary">
                        <WatchLaterIcon></WatchLaterIcon>
                    </Fab>
                </Tooltip>
            </TableCell>
        </TableRow>
    )
}
