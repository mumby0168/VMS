import React from 'react';
import { TableRow, TableCell, Fab } from '@material-ui/core';
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import ArrowBackIcon from '@material-ui/icons/ArrowBack';


export default function AccessRecordsTemplate(props) {

    const arrow = props.record.action === "in" ? <ArrowForwardIcon style={{ color: 'green' }} /> : <ArrowBackIcon style={{ color: 'red' }} />

    return (
        <TableRow>
            <TableCell>
                <Fab disabled>
                    {arrow}
                </Fab>
            </TableCell>
            <TableCell>{props.record.date}</TableCell>
            <TableCell>{props.record.time}</TableCell>
            <TableCell>{props.record.siteName ?? "---"}</TableCell>
        </TableRow>
    )
}