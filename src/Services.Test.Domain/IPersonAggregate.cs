namespace Services.Test.Domain
{
    public interface IPersonAggregate
    {
        Person CreatePerson(string firstName, int age);
    }
}