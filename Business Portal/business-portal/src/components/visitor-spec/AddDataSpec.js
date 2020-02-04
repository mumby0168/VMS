import React from 'react'
import CardDialog from '../../common/CardDialog'
import { useDispatch } from 'react-redux';
import { Button, TextField, Select, MenuItem, FormControl, InputLabel, makeStyles } from '@material-ui/core';
import { closeAdd, updateForm, clearAddSpecForm, createDataSpec } from '../../actions/specificationActions';

const useStyles = makeStyles(theme => ({
    seperation: {
        margin: theme.spacing(3)
    }
}))

export default function AddUserSpec(props) {

    const classes = useStyles();
    const { options, open, form } = props;
    const dispatch = useDispatch();

    const handleClose = () => {
        dispatch(closeAdd());
        dispatch(clearAddSpecForm());

    }

    const handleSubmit = (e) => {
        e.preventDefault();
        createDataSpec(dispatch, form.label, form.message, form.code);
    }

    const optionItems = options.map((opt, index) => {
        return <MenuItem key={index} value={opt}>{opt}</MenuItem>
    })

    const selectionChange = (e) => {
        dispatch(updateForm(e.target.value, "code"));
    }

    return (
        <CardDialog
            title="Add Data Specification"
            maxWidth="sm"
            handleClose={handleClose}
            showCancel
            open={open}
            text="Please create another data entry by completing the form below"
            actions={
                <Button variant="contained" color="primary" onClick={handleSubmit}>Create</Button>
            }
        >



            <TextField
                required
                classname={classes.seperation}
                fullWidth
                value={form.label}
                name="label"
                label="Label"
                onChange={(e) => dispatch(updateForm(e.target.value, e.target.name))}
            />

            <FormControl fullWidth classname={classes.seperation}>
                <InputLabel id="validation-opt">Validation</InputLabel>
                <Select value={form.code} onChange={selectionChange} labelId="validation-opt">
                    {optionItems}
                </Select>
            </FormControl>

            <TextField
                required
                classname={classes.seperation}
                fullWidth
                value={form.message}
                name="message"
                label="Validation Message"
                onChange={(e) => dispatch(updateForm(e.target.value, e.target.name))}
            />




        </CardDialog>
    )
}
