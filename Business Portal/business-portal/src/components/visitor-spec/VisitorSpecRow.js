import React from 'react'
import { TableRow, TableCell, Tooltip, Fab, Popover, Typography } from '@material-ui/core'
import { Delete, Reorder } from '@material-ui/icons'
import { useDispatch } from 'react-redux'
import { deprecateDataSpec } from '../../actions/specificationActions';
import ReorderSpec from './ReorderSpec';

export default function VisitorSpecRow(props) {

    const dispatch = useDispatch();

    const [anchorToElement, setAnchorElement] = React.useState(null);

    const deprecate = () => {
        deprecateDataSpec(dispatch, props.spec.id);
    }

    const showPopup = event => setAnchorElement(event.currentTarget);

    const closePopup = () => setAnchorElement(null);

    const open = Boolean(anchorToElement);



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
                <Tooltip title="Deprecate">
                    <Fab onClick={deprecate} color="primary">
                        <Delete />
                    </Fab>
                </Tooltip>
                <Tooltip title="Change order">
                    <Fab onClick={showPopup} color="primary">
                        <Reorder />
                    </Fab>
                </Tooltip>
                <Popover
                    open={open}
                    onClose={closePopup}
                    anchorEl={anchorToElement}
                    anchorOrigin={{
                        vertical: 'bottom',
                        horizontal: 'center',
                    }}
                    transformOrigin={{
                        vertical: 'top',
                        horizontal: 'center',
                    }}
                >
                    <ReorderSpec id={props.spec.id} current={props.spec.order} updateOrder={props.updateOrder} max={props.maxOrder}/>
                </Popover>
            </TableCell>
        </TableRow>
    )
}
