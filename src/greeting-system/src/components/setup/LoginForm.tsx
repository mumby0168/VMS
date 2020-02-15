import React, { ReactElement } from 'react'
import { TextField, makeStyles, Theme, createStyles, Button } from '@material-ui/core'
import { ISetupForm } from '../../redux/actions/setupActions'
import {Alert} from '@material-ui/lab'

interface Props {
    updateForm(data: ISetupForm): void;
    formData: ISetupForm;
    login(data: ISetupForm): void;
    error: string;
}

const useStyles = makeStyles((theme: Theme) => createStyles({
    textFieldSpacing: {
        marginTop: theme.spacing(1),
        marginBottom: theme.spacing(1)
    }
}))

export default function LoginForm(props: Props): ReactElement {

    const classes = useStyles()

    const execute = (e: any) => {
        e.preventDefault();
        props.login(props.formData);
    }

    const err = props.error !== "" ? <Alert variant="filled" severity="error">{props.error}</Alert> : ''


    return (

        <form>
            <TextField name='b-code' onChange={(e) => props.updateForm({...props.formData, code: e.target.value})} value={props.formData.code} className={classes.textFieldSpacing} fullWidth type="number" label="Business Code" />
            <TextField required name='email' onChange={(e) => props.updateForm({...props.formData, email: e.target.value})} value={props.formData.email} className={classes.textFieldSpacing} fullWidth type="email" label="Email Address" />
            <TextField required name='password' onChange={(e) => props.updateForm({...props.formData, password: e.target.value})} value={props.formData.password} className={classes.textFieldSpacing} fullWidth type="password" label="Password" />
            {err}
            <div className="center">
           
            <Button type="submit" onClick={execute} className={classes.textFieldSpacing} variant="contained" color="primary">Login</Button>
            
            </div>
        </form>
    )
}
