namespace Services.RabbitMq.Interfaces.Wrappers
{
    public interface IJsonConvertWrapper
    {
        string Serialize(object obj);

        T Deserialize<T>(string json);
    }
}
