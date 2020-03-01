using System;
using Services.Common.Domain;

namespace Services.Test.Domain
{
    public class Person : IDomain
    {
        public Guid Id { get; internal set; }
        
        public string FirstName { get; internal set; }
        
        public int Age { get; internal set; }

        internal Person()
        {
            Id = Guid.NewGuid();
        }
    }
}