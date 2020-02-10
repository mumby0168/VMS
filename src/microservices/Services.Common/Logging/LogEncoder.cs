using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Logging
{
    public class LogEncoder : ILogEncoder
    {
        public byte[] EncodeLog(string serviceName, string category, LogType type, string message, Guid operationId, Guid userId)
        {
            var bytes = new List<byte>();

            bytes.Add(Convert.ToByte(type));

            var name = serviceName.GetBytes();
            bytes.AddRange(name.Length.ToUShort().GetBytes());
            bytes.AddRange(name);

            var cat = category.GetBytes();
            bytes.AddRange(cat.Length.ToUShort().GetBytes());
            bytes.AddRange(cat);

            var mes = message.GetBytes();
            bytes.AddRange(mes.Length.ToUShort().GetBytes());
            bytes.AddRange(mes);

            bytes.AddRange(operationId.ToByteArray());
            bytes.AddRange(userId.ToByteArray());

            return bytes.ToArray();
        }
    }
}
