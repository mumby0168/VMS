import React, { ReactElement } from 'react'
import { TextField, makeStyles, Theme, createStyles, Button } from '@material-ui/core'
import { ISetupForm } from '../../redux/actions/setupActions'

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

    const err = props.error !== "" ? <div><br/><span>{props.error}</span></div> : ''


    return (

        <form>
            <TextField onChange={(e) => props.updateForm({...props.formData, code: e.target.value})} value={props.formData.code} className={classes.textFieldSpacing} fullWidth type="number" label="Business Code" />
            <TextField onChange={(e) => props.updateForm({...props.formData, email: e.target.value})} value={props.formData.email} className={classes.textFieldSpacing} fullWidth type="email" label="Email Address" />
            <TextField onChange={(e) => props.updateForm({...props.formData, password: e.target.value})} value={props.formData.password} className={classes.textFieldSpacing} fullWidth type="password" label="Password" />
            {err}
            <div className="center">
           
            <Button onClick={execute} className={classes.textFieldSpacing} variant="contained" color="primary">Login</Button>
            
            </div>
        </form>
    )
}
