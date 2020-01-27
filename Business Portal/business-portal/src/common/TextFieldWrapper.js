import React from 'react'
import { TextField } from '@material-ui/core'

export default function TextFieldWrapper(props) {
    return props.isValid ? <TextField  {...props}/> : <TextField error {...props}/>
}
