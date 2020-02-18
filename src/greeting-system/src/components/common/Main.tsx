import React from 'react'
import { Paper, Grid } from '@material-ui/core'
import { Time, } from '../main/Time'

interface IMainProps {


}

export class Main extends React.Component<IMainProps> {


    public render() {
        return (
            <Paper className="hundred-percent" >
                <div className="hundred-percent">
                    <div>
                        <Grid alignItems="center"  container spacing={2}>
                            <Grid style={{textAlign: 'center'}} item xs={4}>
                                <Time></Time>
                            </Grid>
                            <Grid style={{textAlign: 'center'}} item xs={4}>
                                Business Data
                            </Grid>
                            <Grid style={{textAlign: 'center'}} item xs={4}>
                                Date
                            </Grid>
                        </Grid>
                    </div>
                    <div style={{ height: '80%' }} >
                        {this.props.children}
                    </div>
                </div>
            </Paper>
        )
    }
}