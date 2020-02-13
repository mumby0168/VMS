import React, { ReactElement } from 'react'
import { TextField, makeStyles, Theme, createStyles, Button } from '@material-ui/core'

interface Props {

}

const useStyles = makeStyles((theme: Theme) => createStyles({
    textFieldSpacing: {
        marginTop: theme.spacing(1),
        marginBottom: theme.spacing(1)
    }
}))

export default function LoginForm({ }: Props): ReactElement {

    const classes = useStyles()


    return (
        <form>
            <TextField className={classes.textFieldSpacing} fullWidth type="number" label="Business Code" />
            <TextField className={classes.textFieldSpacing} fullWidth type="email" label="Email Address" />
            <TextField className={classes.textFieldSpacing} fullWidth type="password" label="Password" />
            <div className="center">
            <Button className={classes.textFieldSpacing} variant="contained" color="primary">Login</Button>
            </div>
        </form>
    )
}
