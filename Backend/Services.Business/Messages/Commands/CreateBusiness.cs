using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Common.Names;
using Services.RabbitMq.Attributes;
using Services.RabbitMq.Interfaces.Messaging;

namespace Services.Businesses.Messages.Commands
{
    [MicroService(ServiceNames.Gateway)]
    public class CreateBusiness : ICommand
    {
        public string Name { get; }

        public string TradingName { get; }

        public string WebAddress { get; }

        public string HeadContactFirstName { get; }

        public string HeadContactSecondName { get; }

        public string HeadContactContactNumber { get; }

        public string HeadContactEmail { get; }

        public string HeadOfficePostCode { get; }

        public string HeadOfficeAddressLine1 { get; }

        public string HeadOfficeAddressLine2 { get; }

        [JsonConstructor]
        public CreateBusiness(string name, string tradingName, string webAddress, string headContactFirstName, string headContactSecondName, string headContactContactNumber, string headContactEmail, string headOfficePostCode, string headOfficeAddressLine1, string headOfficeAddressLine2)
        {
            Name = name;
            TradingName = tradingName;
            WebAddress = webAddress;
            HeadContactFirstName = headContactFirstName;
            HeadContactSecondName = headContactSecondName;
            HeadContactContactNumber = headContactContactNumber;
            HeadContactEmail = headContactEmail;
            HeadOfficePostCode = headOfficePostCode;
            HeadOfficeAddressLine1 = headOfficeAddressLine1;
            HeadOfficeAddressLine2 = headOfficeAddressLine2;
        }

        //public CreateBusiness()
        //{
            
        //}

    }
}
