import React from 'react'
import { Avatar, Typography, Fab, Tooltip, TableRow, TableCell, makeStyles } from '@material-ui/core'
import WatchLaterIcon from '@material-ui/icons/WatchLater';
import { useDispatch } from 'react-redux';
import { openEmployeeRecords } from '../../actions/employeeActions';



const useStyles = makeStyles(theme => ({
    avatar: {
        marginBottom: theme.spacing(0.5)
    }
}));

export default function EmployeeRow() {

    const dispatch = useDispatch();

    const classes = useStyles();

    const showDialog = () => {
        dispatch(openEmployeeRecords(null))
    }

    return (
        <TableRow>
            <TableCell>
                <div style={{float: 'left'}}>
                    <div align="center">
                        <Avatar className={classes.avatar}>EG</Avatar>
                        <Typography>Elliot Giles</Typography>
                    </div>
                </div>
            </TableCell>
            <TableCell>
                <Typography>Brough</Typography>
            </TableCell>
            <TableCell>
                <Typography>In (09:55)</Typography>
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
