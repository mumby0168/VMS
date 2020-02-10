import React from 'react'
import CardDialog from '../../common/CardDialog';
import { hideAddEmployee, createEmployee } from '../../actions/employeeActions';
import { Button, TextField } from '@material-ui/core';
import Alert from '@material-ui/lab/Alert';


export default function AddEmployee(props) {

    const [value, setValue] = React.useState("");

    const state = props.data;    

    const submitForm = () => {
        props.dispatch(createEmployee(value));        
    }

    const handleChange = (e) => {
        setValue(e.target.value);
    }

    const handleClose = () => {
        props.dispatch(hideAddEmployee())
        setValue("");
    }

    const error = props.error === "" ? "" : <Alert severity="error">{props.error}</Alert>


    return (
        <CardDialog
            maxWidth="sm"
            handleClose={handleClose} title="Add Employee"
            showCancel
            open={state.isOpen}
            text="Please add another employee to your system by entering there email where they will recieve an email to finish setting up there account."
            actions={
                <Button variant="contained" color="primary" onClick={submitForm}>Submit</Button>
            }>            
            <TextField style={{paddingBottom: '0.5rem'}} value={value} onChange={handleChange}/>
            {error}

        </CardDialog>
    )
}
