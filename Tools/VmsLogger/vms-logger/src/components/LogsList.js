import React, { Component } from 'react'
import { connect } from 'react-redux'
import LogItem from './LogItem';

class LogsList extends Component {

    render() {


        const logElements = this.props.logs.map((log) =>
            <LogItem log={log}></LogItem>
        );

        const logs = (
            <div className="log-list-wrapper">
                <ul className="list-group">

                    <li className="list-group-item log-header-item">
                        <div className="row">
                            <div className="col-md-1">
                                <p>UTC Stamp</p>
                            </div>
                            <div className="col-md-1">
                                <p>Type</p>
                            </div>
                            <div className="col-md-2">
                                <p>Service</p>
                            </div>
                            <div className="col-md-1">
                                <p>Category</p>
                            </div>
                            <div className="col-md-5">
                                <p>Message</p>
                            </div>
                        </div>
                    </li>

                    {logElements}
                </ul>
            </div>
        )

        return this.props.fetching ? (<h1>Loading</h1>) : logs;
    }
}

const mapStateToProps = (state) => {
    return {
        logs: state.logs.logs,
        fetching: state.logs.fetching,
        error: state.logs.error
    }
}

export default connect(mapStateToProps)(LogsList);
