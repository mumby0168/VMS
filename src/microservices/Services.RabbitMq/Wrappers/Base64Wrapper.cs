using System;
using Services.RabbitMq.Interfaces.Wrappers;

namespace Services.RabbitMq.Wrappers
{
    public class Base64Wrapper : IBase64Wrapper
    {
        public string ToBase64(byte[] data) => Convert.ToBase64String(data);

        public byte[] FromBase64(string base64Data) => Convert.FromBase64String(base64Data);
    }
}
