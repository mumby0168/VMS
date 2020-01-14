import React from 'react';
import { TableContainer, Table, TableHead, TableRow, TableCell, TableBody } from '@material-ui/core';
import AccessRecordsTemplate from './AccessRecordTemplate';


export default function AccessRecordsList(props) {

    const rows = props.records.map((record) => <AccessRecordsTemplate key={record.id} record={record}></AccessRecordsTemplate>)

    return (
        <TableContainer style={{maxHeight: '650px', overflowY: "auto"}}>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>Date</TableCell>
                        <TableCell>Time</TableCell>
                        <TableCell>Site</TableCell>
                        <TableCell>In/Out</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {rows}
                </TableBody>
            </Table>
        </TableContainer>
    )
}