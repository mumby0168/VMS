import { IOperation, operationsHub, OperationStatus } from "./operationsManager";



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

    public listen(id: string, handleSucess: (op: IOperation) => void, handleFailure: (op: IOperation) => void = this.defaultFailureHandler) {
        
        var counter = 0;
        const intervalId = setInterval(() => {
            
            if(counter > 3000) {
                clearInterval(intervalId);
                console.error('failed to get operation feedback');
            }

            const op = this.operations.find(op => op.id === id);
            if(op) {

                const index = this.operations.indexOf(op);
                this.operations.splice(index);

                op.status === OperationStatus.Completed ? handleSucess(op) : handleFailure(op);                

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

export const operationsAggregator = new OperationsAggregator();
