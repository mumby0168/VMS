import React from 'react'
import { TableRow, TableCell, Tooltip, Fab } from '@material-ui/core'
import { Delete } from '@material-ui/icons'
import { useDispatch } from 'react-redux'
import { deprecateDataSpec } from '../../actions/specificationActions';

export default function VisitorSpecRow(props) {

    const dispatch = useDispatch();

    const deprecate = () => {
        deprecateDataSpec(dispatch, props.spec.id);
    }



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
                    <Fab onClick={deprecate} color="primary">
                        <Delete/>
                    </Fab>
                </Tooltip>
            </TableCell>
        </TableRow>
    )
}
