using System;
using System.Collections.Generic;
using System.Text;

namespace App.Shared.Exceptions
{
    public class OperationNotFoundException : Exception
    {
        public OperationNotFoundException(Guid operationId)
        {
            OperationId = operationId;
        }

        public Guid OperationId { get; }
    }
}
