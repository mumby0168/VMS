import React, { Component } from 'react'
import { Backdrop, Typography, CircularProgress } from '@material-ui/core'
import { IAppState } from '../../redux/store'
import { connect } from 'react-redux'
import '../styles/Overlay.css'

interface IOverlayProps {
    open: boolean;
    message: string;
    showSpinner: boolean;
}

const mapStateToProps = (state: IAppState) => {
    return {
        open: state.overlay.open,
        message: state.overlay.message,
        showSpinner: state.overlay.showSpinner        
    }
}   


class Overlay extends Component<IOverlayProps> {
    render() {

        console.log('Overlay props:');
        console.log(this.props)

        const spinner = this.props.showSpinner ? <CircularProgress /> : ""

        return (
            <Backdrop id="z-max" open={this.props.open}>
                <div className='backdrop-grid'>
                    <div className="backdrop-item">
                        {spinner}
                        <Typography style={{marginTop: '1rem'}} variant="h4">{this.props.message}</Typography>
                    </div>
                </div>
            </Backdrop>
        )
    }
}

export default connect(mapStateToProps)(Overlay);
