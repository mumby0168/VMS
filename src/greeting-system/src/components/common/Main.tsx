import React from 'react'
import { Paper, Grid, Card } from '@material-ui/core'
import { Time, } from '../main/Time'

interface IMainProps {


}

export class Main extends React.Component<IMainProps> {


    public render() {
        return (
            <Paper className="hundred-percent background" >
                <div className="hundred-percent opaque ">
                    <div style={{height: '10%'}} className="main-header">
                    <Card style={{padding: '4px'}}>
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
                    </Card>
                    </div>

                        {this.props.children}                
                </div>
            </Paper>
        )
    }
}