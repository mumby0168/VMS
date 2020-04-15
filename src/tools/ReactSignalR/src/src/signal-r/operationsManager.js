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
            this.connection.invoke("connect", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6ImItYWRtaW5AdGVzdC5jb20iLCJuYW1laWQiOiI4MGZlMDBmMy01ZDQ5LTQzODEtOTgzNS00ZjFiZGUzY2VjY2IiLCJyb2xlIjoiQnVzaW5lc3NBZG1pbiIsImJ1c2luZXNzSWQiOiI4NjFhNTcyZS1mZDg5LTQ5YWEtYmRlMC05OWJmZDY0ZDhmZWIiLCJuYmYiOjE1ODQzNjY4MDUsImV4cCI6MTU4NDM3NzYwNSwiaWF0IjoxNTg0MzY2ODA1fQ.nBusPJ1M98lcGGPMFhd6HtuottwRnRWM1FNAptCJZmegCqJaEMuYEAZPLO1nblT6S50dG4K_AlQFrnrlz84H6w").then(() => console.log("Sending connect messsage")).catch(err => console.log(err));
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