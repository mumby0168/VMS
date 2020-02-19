import React from 'react'
import { Paper, Grid, Card, Button } from '@material-ui/core'
import { Time, } from '../main/Time'
import { ISite } from '../../redux/reducers/systemReducer'
import { IAppState } from '../../redux/store'
import { connect } from 'react-redux'
import { SystemViews, viewChangedAction } from '../../redux/actions/systemActions'
import { InitialSignIn } from '../../hocs/InitialSignIn'
import Keypad from '../../hocs/Keypad'
import StaffSelect from '../../hocs/StaffSelect'

interface IMainProps {
    site: ISite
    view: SystemViews;
    navigate: (view : SystemViews) => void;
}

 class Main extends React.Component<IMainProps> {
   

    renderInternals() {
        switch(this.props.view) {
            case SystemViews.INIT_SIGN_IN: return <InitialSignIn naviagate={this.props.navigate}/>
            case SystemViews.STAFF_KEYPAD: return <Keypad/>
            case SystemViews.STAFF_SELECT: return <StaffSelect/>
            default: return <h1>404 Not Found</h1>
        }
    }

    public render() {

        const internal = this.renderInternals();

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
                    <div style={{height: '80%'}}>
                        {internal}
                    </div>
                    <div className="opaque main-footer">
                        <div className="main-footer-grid">                               
                                <div className="main-footer-back-button">
                                    <Button onClick={(e) => this.props.navigate(SystemViews.INIT_SIGN_IN)} variant="contained">Back</Button>
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
        site: state.system.site,
        view: state.system.systemView
    }
}

const mapDispatch = (dispatch: any) => {
    return {
        navigate: (view: SystemViews) => dispatch(viewChangedAction(view)),
    }
}

export default connect(mapStateToProps, mapDispatch)(Main);