import React, { Component } from 'react'
import {connect} from 'react-redux'

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

        const operations = this.props.operations.map((operation) => {
           return  <h4>1</h4>
        });

        return (
            <div>
                {operations}
            </div>
        )
    }   
}


const mapStateToProprs = (state) => {
    console.log(state);
    return {
        operations: state.operations.operations
    }
}

export default connect(mapStateToProprs)(Messages)
