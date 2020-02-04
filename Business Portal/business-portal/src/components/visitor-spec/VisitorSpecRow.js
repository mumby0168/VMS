import React from 'react'
import { TableRow, TableCell, Tooltip, Fab } from '@material-ui/core'
import { Delete } from '@material-ui/icons'

export default function VisitorSpecRow(props) {
    return (
        <TableRow>
            <TableCell>
                {props.spec.order}
            </TableCell>
            <TableCell>
                {props.spec.label}
            </TableCell>
            <TableCell>
                {props.spec.validationCode}
            </TableCell>
            <TableCell>
                {props.spec.validationMessage}
            </TableCell>
            <TableCell>
                <Tooltip  title="Deprecate">
                    <Fab color="primary">
                        <Delete/>
                    </Fab>
                </Tooltip>
            </TableCell>
        </TableRow>
    )
}
