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
                var counter = 0;
                var handlerLocal = handler;

                var callback = setInterval(() => {
                    counter++;                    

                    if (counter > 3) {
                        console.log("Timeout for push expired");
                        this.props.dispatch(getOperationStatus(handler));
                        clearInterval(callback);
                    }

                    var result = this.props.pendingOperations.find(p => p.id === handlerLocal.id);
                    if (result !== undefined || null) {
                        clearInterval(callback);
                        this.props.dispatch({ type: "REMOVE_HANDLE", payload: callback });
                        console.log(handlerLocal);

                        if (result.status === "failed") {
                            handlerLocal.action.payload.message = result.reason;
                            handlerLocal.action.payload.failed = true;                                                        
                        }
                    

                        if (handlerLocal.completionAction !== null && result.status !== "failed") {                                                        
                            handlerLocal.completionAction();
                        }                        

                        this.props.dispatch(hideSiteSpinner());
                        this.props.dispatch(handlerLocal.action);
                    }
                }, 300);
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
