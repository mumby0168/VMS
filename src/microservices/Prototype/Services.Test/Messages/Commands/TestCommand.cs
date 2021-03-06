﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Test.Messages.Commands
{
    public class TestCommand : ICommand
    {
        public bool IsPass { get; }

        [JsonConstructor]
        public TestCommand(bool isPass)
        {
            IsPass = isPass;
        }
    }
}
