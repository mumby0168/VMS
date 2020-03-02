using System;

namespace Services.Test.Domain
{
    public class PersonAggregate : IPersonAggregate
    {
        public Person CreatePerson(string firstName, int age)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                throw new Exception("first name cannot be empty");
            }

            if (age < 0)
            {
                throw new Exception("Age must be over the zero");
            }
            
            return new Person()
            {    
                FirstName = firstName,
                Age = age
            };
        }
    }
}