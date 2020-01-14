import * as signalR from '@microsoft/signalr'
import {addOperation,signalRConnectionUpdate} from '../actions/operationsActions.js'

export default class OperationsManager {

    connection = null;    

    constructor(){
        this.connection = new signalR.HubConnectionBuilder().withUrl("http://localhost:5015/operations").build();         
    }


    connect(dispatchHandle, jwt) {                                
        this.connection.start().then(() => {            
            this.connection.invoke("connect", jwt)
            .then(() => {                                
                console.log("connection request send to push service.")                
            })
            .catch(err => {                                
                dispatchHandle(signalRConnectionUpdate(false));            
            });

        }).catch(err =>  { 
            dispatchHandle(signalRConnectionUpdate(false));                   
        });    

        this.connection.on("connectionSucceeded", () => {
            dispatchHandle(signalRConnectionUpdate(true));     
        });

        this.connection.on("connectionFailed", (reason) => {
            console.log("Connection Failed to push for reason: " + reason);
            dispatchHandle(signalRConnectionUpdate(false));     
        })
        

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