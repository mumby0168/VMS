import React from 'react'
import { Paper, Grid, Card, Button } from '@material-ui/core'
import { Time, } from '../main/Time'
import { ISite } from '../../redux/reducers/systemReducer'
import { IAppState } from '../../redux/store'
import { connect } from 'react-redux'

interface IMainProps {
    site: ISite
}

 class Main extends React.Component<IMainProps> {


    public render() {
        return (
            <Paper className="hundred-percent background" >
                <div className="hundred-percent opaque ">
                    <div style={{ height: '5%' }} className="main-header">
                        <Card>
                            <Grid alignItems="center" container spacing={2}>
                                <Grid style={{ textAlign: 'center' }} item xs={4}>
                                    <Time></Time>
                                </Grid>
                                <Grid style={{ textAlign: 'center' }} item xs={4}>
                                    {this.props.site.name}
                            </Grid>
                                <Grid style={{ textAlign: 'center' }} item xs={4}>
                                    Date
                            </Grid>
                            </Grid>
                        </Card>
                    </div>
                    {this.props.children}
                    <div className="opaque main-footer">
                        <div className="main-footer-grid">                               
                                <div className="main-footer-back-button">
                                    <Button variant="contained">Back</Button>
                                </div>
                                <div className="main-footer-menu-button">
                                    <Button variant="contained">Settings</Button>
                                </div>
                        </div>                        
                    </div>
                </div>
            </Paper>
        )
    }
}

const mapStateToProps = (state: IAppState) => {
    return {
        site: state.system.site
    }
}

export default connect(mapStateToProps)(Main);