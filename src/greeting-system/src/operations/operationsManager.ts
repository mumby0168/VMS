import * as push from '@microsoft/signalr'
import { systemBaseIp } from '../redux/api/helpers';

class OperationsManager {

    private readonly _url : string;
    private readonly connection: push.HubConnection;
    private connected: boolean;    
    private successHandle: ((op: IOperation) => void | undefined) | undefined;
    private failedHandle: ((op: IOperation) => void | undefined) | undefined;
    private handleRegistered: boolean;


    constructor(url : string) {
        this._url = url;
        this.connection = new push.HubConnectionBuilder().withUrl(this._url).build();        
        this.connected = false;
        this.handleRegistered = false;                        
    }

    public getConnectionStatus(): boolean {
        return this.connected;
    }

    public shutdown() {
        this.connection.stop();
    }

    public startConnection(jwt: string) {        
        
        this.connection.start()
        .then(() => this.handleConnection(jwt))
        .catch((err) => this.handleConnectionFailure(err, jwt));        
    }

    public registerForOperationUpdates(success: (op: IOperation) => void, failed: (op: IOperation) => void) {
        this.successHandle = success;
        this.failedHandle = failed;
        this.handleRegistered = true;
    }
       
    private handleConnection(jwt: string) {
        this.connection.invoke("connect", jwt);

        this.connection.on("connectionSucceeded", () => this.connected = true)

        this.connection.on("connectionFailed", (reason) => {
            console.log('push connection issue: ' + reason);
            this.connected = false;
        });
        this.handleMessages();
    }

    private handleMessages() {        
        if(this.successHandle && this.failedHandle) {
            this.connection.on('operationComplete', this.successHandle);
            this.connection.on('operationFailed', this.failedHandle);
        }
        else {
            console.error('No handlers for messages have been defined.')            
        }        
    }    


    private handleConnectionFailure(error: any, jwt: string) {
        console.error(error);
        //TODO: may need to retry connection here.
    }
}

export enum OperationStatus {
    Failed = 1,
    Completed = 2,
}

export interface IOperation {
    id: string;
    status: OperationStatus;
    reason: string | null;
    code: string | null;
}

export const operationsHub = new OperationsManager(`http://${systemBaseIp}:5015/operations`);
