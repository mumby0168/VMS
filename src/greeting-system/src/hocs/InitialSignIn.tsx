
import React from 'react'
import {Grid} from '@material-ui/core'


export const InitialSignIn = () => {
    return (
        <div className="center">
            <Grid direction="column" container>
                <Grid item>
                    Vis Sign In
                </Grid>
                <Grid item>
                    Staff Sign In
                </Grid>
            </Grid>
        </div>
    )
}