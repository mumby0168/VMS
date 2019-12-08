import * as signalR from '@microsoft/signalr'
import {addOperation} from '../actions/operationsActions'

export default class OperationsManager {

    connection = null;    

    constructor(){
        this.connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/operations").build();         
    }


    connect(dispatchHandle) {        
        console.log("connecting");        

        this.connection.start().then(() => {
            console.log("connected");
            this.connection.invoke("connect", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InRlc3RAdGVzdC5jb20iLCJuYW1laWQiOiIwM2I0NTIxYi0wYmQ1LTQ1M2UtOGIwZC03NGUwYTVlMThkMDkiLCJyb2xlIjoiU3lzdGVtQWRtaW4iLCJuYmYiOjE1NzU1MzkwNzUsImV4cCI6MTU3NTU0OTg3NSwiaWF0IjoxNTc1NTM5MDc1fQ.KOiGeiCugnUpNBSZKWL6LvBk4Q2k4aYIcaFLYnEIS1IEnZUrLnGmW7_x3mb9WjhQNbJam4_Et8rtMQPzw1RRUw").then(() => console.log("Sending connect messsage")).catch(err => console.log(err));
        }).catch(err => console.log(err));

        this.connection.on("operationComplete", (op) => {
            console.log("operation complete received.");
            console.log(op);            
            dispatchHandle(addOperation({
                status: "complete",
                id: op.operationId,
            }));   
        });

        this.connection.on("operationFailed", (op) => {
            console.log("operation failed received.");
            console.log(op);
            dispatchHandle(addOperation({
                status: "failed",
                id: op.operationId,
                code: op.code,
                reason: op.reason
            }));           
        });
        

        
    }
}