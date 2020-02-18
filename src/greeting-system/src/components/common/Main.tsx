import React from 'react'
import { Paper, Grid } from '@material-ui/core'
import { Time, } from '../main/Time'

interface IMainProps {


}

export class Main extends React.Component<IMainProps> {


    public render() {
        return (
            <Paper className="full-height" >
                <Grid className="full-height" container direction="row">
                    <Grid style={{ height: '20%' }} item md={12}>
                        <Grid spacing={2} alignItems="center" container direction="column">
                            <Grid item md>
                                <Time></Time>
                            </Grid>
                            <Grid item md={6}>
                                Data 
                            </Grid>
                            <Grid item md>

                            </Grid>
                        </Grid>
                    </Grid>
                    <Grid style={{ height: '80%' }} item md={12}>
                        {this.props.children}
                    </Grid>
                </Grid>

            </Paper>
        )
    }
}