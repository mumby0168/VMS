﻿using System.Threading.Tasks;

namespace Services.RabbitMq.Interfaces.Messaging
{
    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand message, IRequestInfo requestInfo);
    }
}
