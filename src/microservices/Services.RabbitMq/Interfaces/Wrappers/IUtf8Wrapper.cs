namespace Services.RabbitMq.Interfaces.Wrappers
{
    public interface IUtf8Wrapper
    {
        byte[] GetBytes(string @string);

        string GetString(byte[] bytes);
    }
}
