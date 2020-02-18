import React from 'react'
import { Paper, Grid } from '@material-ui/core'
import { Time, } from '../main/Time'

interface IMainProps {


}

export class Main extends React.Component<IMainProps> {


    public render() {
        return (
            <Paper className="hundred-percent" >
                <Grid className="hundred-percent" container direction="row">
                    <Grid item md={12}>
                        <Grid spacing={2} container direction="column">
                            <Grid item xs={4}>
                                <Time></Time>
                            </Grid>
                            <Grid item xs={6}>
                                Business Data
                            </Grid>
                            <Grid item xs={4}>
                                Date
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