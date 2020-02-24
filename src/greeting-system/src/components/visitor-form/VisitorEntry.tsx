import React, { ReactElement } from 'react'
import { TextField } from '@material-ui/core'
import { IVisitorDataSpecification } from '../../redux/actions/visitorFormActions'

interface IVisitorEntryProps {
    entry: IVisitorDataSpecification;
    index: number;
    updateHandle: (index: number, newValue: string) => void;
}

export default function VisitorEntry({ entry, index, updateHandle }: IVisitorEntryProps): ReactElement {
    return (
        <TextField
            fullWidth
            label={entry.label}
            value={entry.value}
            name={index.toString()}
            onChange={(e) => updateHandle(Number.parseInt(e.target.name), e.target.value)} />
    )
}
