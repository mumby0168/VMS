import React from 'react';
import { TableContainer, Table, TableHead, TableRow, TableCell, TableBody, Fab } from '@material-ui/core';
import AccessRecordsTemplate from './AccessRecordTemplate';
import ImportExportIcon from '@material-ui/icons/ImportExport';


export default function AccessRecordsList(props) {

    const rows = props.records.map((record) => <AccessRecordsTemplate key={record.id} record={record}></AccessRecordsTemplate>)

    const body = rows.length > 0 ? rows :
        <TableRow>
            <TableCell>
                <h2>No access records</h2>
            </TableCell>
        </TableRow>

    return (
        <TableContainer>
            <Table>
                <TableHead>
                    <TableRow>
                        <TableCell>
                            <Fab disabled>
                                <ImportExportIcon />
                            </Fab>
                        </TableCell>
                        <TableCell>Date</TableCell>
                        <TableCell>Time</TableCell>
                        <TableCell>Site</TableCell>
                    </TableRow>
                </TableHead>
                <TableBody>
                    {body}
                </TableBody>
            </Table>
        </TableContainer>
    )
}