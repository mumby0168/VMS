import { IOperation, operationsHub, OperationStatus } from "./operationsManager";
import { gatewayClient } from "../redux/api/helpers";



class OperationsAggregator {

    private operations: IOperation[];

    constructor() {
        this.operations = [];        
    }


    public start(jwt: string) {
        operationsHub.registerForOperationUpdates(
            (op) => this.operations.push(op),
            (op) => this.operations.push(op)
        );
        operationsHub.startConnection(jwt);
    }

    public listen(id: string, handleSuccess: (op: IOperation) => void, handleFailure: (op: IOperation) => void = this.defaultFailureHandler) {
        
        var counter = 0;
        const intervalId = setInterval(() => {
            
            if(counter > 3000) {
                clearInterval(intervalId);
                console.log("Handling response using http connection.")
                handleWithHttp(id, handleSuccess, handleFailure)
            }

            const op = this.operations.find(op => op.id === id);
            if(op) {

                const index = this.operations.indexOf(op);
                this.operations.splice(index);

                op.status === OperationStatus.Completed ? handleSuccess(op) : handleFailure(op);

                clearInterval(intervalId);
            }
            else {
                counter += 500;
            }

        }, 500)
    }

    private defaultFailureHandler(op: IOperation) {
        console.error(op);
    }
}


const handleWithHttp = (id: string, sucess: (op: IOperation) => void, failure: (op: IOperation) => void) => {
    gatewayClient().get<IOperation>(`operations/${id}`)
    .then((res) => {
        if(res.status === 200) {
            res.status === OperationStatus.Completed ? sucess(res.data) : failure(res.data);                
        }
    })
    .catch((err) => {
        console.error('Operation could not be retrieved.')
    })
}

export const operationsAggregator = new OperationsAggregator();
