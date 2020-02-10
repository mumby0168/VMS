using System.Text;
using Services.RabbitMq.Interfaces.Wrappers;

namespace Services.RabbitMq.Wrappers
{
    public class Utf8Wrapper : IUtf8Wrapper
    {
        public byte[] GetBytes(string @string) => Encoding.UTF8.GetBytes(@string);

        public string GetString(byte[] bytes) => Encoding.UTF8.GetString(bytes);
    }
}
