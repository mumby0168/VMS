import React from 'react';
import Alert from '@material-ui/lab/Alert';
import {Button, TextField, Grid} from '@material-ui/core'
import {loginFormUpdated} from '../../actions/accountActions'

const gridWrapperClass = {
    paddingBottom: '5%'
}

  

export default function LoginForm(props) {

    const error = (props.error === null) ? "" : 
        <Grid style={gridWrapperClass}>
            <Alert severity="error">{props.error}</Alert>
        </Grid>  

    const navigate = () => {        
        props.navigateHandle();
    }

    return (
            <form onSubmit={props.handleSubmit}>
                <Grid direction="column" container>
                    <Grid style={gridWrapperClass}>
                    <TextField value={props.user.username} required id="email" label="Email" onChange={(e) => props.dispatch(loginFormUpdated(e.target.value, props.user.password))}></TextField>
                    </Grid>                            
                    <Grid style={gridWrapperClass}>
                    <TextField value={props.user.password} required id="password" label="Password" type="password"  onChange={(e) => props.dispatch(loginFormUpdated(props.user.username, e.target.value))}></TextField>
                    </Grid>              
                    {error}                  
                    <Grid style={gridWrapperClass}>
                    <Grid style={gridWrapperClass}>
                        <Button onClick={navigate}>Forgotten your password?</Button>
                    </Grid>
                    <Button variant="contained" type="submit">Submit</Button>
                    </Grid>                                                                                   
                </Grid>
            </form>
    )
}