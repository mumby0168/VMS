import React     from 'react';

export default function OperationItem(props) {

    console.log(props.operation)

    if(props.operation.status === "complete") {
        return (
        <li className="list-group-item list-group-item-success">
            <h1>Compelete</h1>
            <p>{props.operation.id}</p>
        </li> 
        )
    }
    else if(props.operation.status === "failed") {
        return (
        <li className="list-group-item list-group-item-danger">       
            <h1>Failed</h1>     
            <h4>{props.operation.code}</h4>
            <h6>{props.operation.reason}</h6>
            <p>{props.operation.id}</p>
        </li> 
        )
    }

}
