import React, { Component } from 'react'
import { connect } from 'react-redux'
import OperationsManager from '../signal-r/operationsManager'

class Operations extends Component {

    manager = null;

    componentWillMount() {
        console.log(this.props);
        this.manager = new OperationsManager();
        this.manager.connect(this.props.dispatch);
    }

    render() {
        return (
            <div>
                
            </div>
        )
    }
}


export default connect()(Operations);
