import React, { Component } from 'react'
import { Backdrop, Typography, CircularProgress } from '@material-ui/core'
import { IAppState } from '../../redux/store'
import { connect } from 'react-redux'
import '../styles/Overlay.css'
import { IconType, closeOverlay, updateOverlayAction } from '../../redux/actions/overlayActions'
import CheckCircleIcon from '@material-ui/icons/CheckCircle';
import CancelIcon from '@material-ui/icons/Cancel';

interface IOverlayProps {
    open: boolean;
    message: string;
    showSpinner: boolean;
    icon: IconType;
    clearButton: boolean;
    handleClose: () => void;
}


const mapStateToProps = (state: IAppState) => {
    return {
        open: state.overlay.open,
        message: state.overlay.message,
        showSpinner: state.overlay.showSpinner,
        icon: state.overlay.iconType,
        clearButton: state.overlay.clearButton
    }
}

const mapDispatch = ((dispatch: any) => {
    return {
        handleClose: () => dispatch(updateOverlayAction(closeOverlay()))
    }
})


class Overlay extends Component<IOverlayProps> {
    render() {
        const icon = () => {
            switch(this.props.icon) {
                case IconType.NONE: return "";
                case IconType.TICK: return <CheckCircleIcon style={{color: 'green', fontSize: 50}}/>
                case IconType.ERROR: return <CancelIcon style={{color: 'red', fontSize: 50}}/>
                default: return "";
            }
        }

        const spinner = this.props.showSpinner && this.props.icon === IconType.NONE ? <CircularProgress /> : ""

        return (
            <Backdrop onClick={this.props.handleClose} id="z-max" open={this.props.open}>
                <div className='backdrop-grid'>
                    <div className="backdrop-item">
                        <div className="backdrop-grid">
                            <div className="backdrop-item">
                                {icon()}
                                {spinner}
                                <Typography style={{ marginTop: '1rem' }} variant="h4">{this.props.message}</Typography>
                            </div>
                        </div>

                    </div>
                </div>
            </Backdrop>
        )
    }
}

export default connect(mapStateToProps, mapDispatch)(Overlay);
