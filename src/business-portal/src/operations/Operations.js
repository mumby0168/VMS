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
                let counter = 0;
                const callback = setInterval(() => {

                    counter++;
                    if (counter > 3) {
                        console.log("Timeout for push expired");
                        this.props.dispatch(getOperationStatus(handler));
                        clearInterval(callback);
                    }

                    console.log("counter: " + counter);
                    console.log(this.props.pendingOperations);

                    const result = this.props.pendingOperations.find(p => p.id === handler.id);

                    console.log(result);

                    if (result !== undefined || null) {
                        clearInterval(callback);
                        if (result.status === "failed") {
                            this.props.dispatch(handler.action.onFailure(result));
                        }
                        else {
                            this.props.dispatch(handler.action.onCompletion(result))
                            console.log("Complete action being called.");
                        }

                        this.props.dispatch(hideSiteSpinner());
                        this.props.dispatch({type: "REMOVE_HANDLE", payload: callback});
                    }
                }, 1000);
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

