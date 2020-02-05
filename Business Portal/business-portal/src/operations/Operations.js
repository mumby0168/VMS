import React, { Component } from 'react'
import { connect } from 'react-redux'
import OperationsManager from '../operations/operationsManager';
import { hideSiteSpinner } from '../actions/uiActions';
import { getOperationStatus } from '../actions/operationsActions';

class Operations extends Component {

    constructor(props) {
        super(props);
        this.manager = new OperationsManager();
    }

    checkConnection = () => {
        if (this.props.connect && !this.props.isPushConnected) {
            this.manager.connect(this.props.dispatch, this.props.jwt);
        }
    }

    handlerCount = 0;


    render() {
        this.checkConnection();
        if (this.props.handlers.length !== this.handlerCount) {

            this.handlerCount = this.props.handlers.length;

            const handler = this.props.handlers[this.handlerCount - 1];

            if (handler !== null || undefined) {
                beginListening(handler, this.props.pendingOperations, this.props.dispatch);
            }
        }


        return (
            <div>

            </div>
        )
    }
}

const mapStateToProps = (state) => {
    return {
        isPushConnected: state.operations.isSignalRConnected,
        connect: state.account.isLoggedIn,
        jwt: state.account.jwtToken,
        pendingOperations: state.operations.pendingOperations,
        handlers: state.operations.operationHandlers
    }
}

export default connect(mapStateToProps)(Operations);


function beginListening(handler, pendingOperations, dispatch) {
    var counter = 0;    
    var callback = setInterval(() => {
        counter++;
        if (counter > 3) {
            console.log("Timeout for push expired");
            dispatch(getOperationStatus(handler));
            clearInterval(callback);
        }
        var result = pendingOperations.find(p => p.id === handler.id);
        if (result !== undefined || null) {
            clearInterval(callback);
            dispatch({ type: "REMOVE_HANDLE", payload: callback });
            console.log(handler);
            if (result.status === "failed") {
                handler.action.payload.message = result.reason;
                handler.action.payload.failed = true;
            }
            if (handler.completionAction !== null && result.status !== "failed") {
                handler.completionAction();
            }
            dispatch(hideSiteSpinner());
            dispatch(handler.action);
        }
    }, 300);
}

