import React, { Component } from 'react'
import {connect} from 'react-redux'
import OperationItem from './OperationItem'

class Messages extends Component {    

    componentWillUpdate()
    {
        console.log(this.props);
    }

    render() {                  
        if(this.props.operations.length === 0)
        {
            return (<h1>No Operations</h1>)
        }        

        const operations = this.props.operations.map((operation) =>
           <OperationItem operation={operation}></OperationItem>
        );

        return (
            <ul className="list-group">
                {operations}
            </ul>
        )
    }   
}


const mapStateToProprs = (state) => {
    console.log(state);
    return {
        operations: state.operations.operationsList
    }
}

export default connect(mapStateToProprs)(Messages)
