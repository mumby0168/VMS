namespace Services.RabbitMq.Interfaces.Wrappers
{
    public interface IBase64Wrapper
    {
        string ToBase64(byte[] data);

        byte[] FromBase64(string base64Data);
    }
}
